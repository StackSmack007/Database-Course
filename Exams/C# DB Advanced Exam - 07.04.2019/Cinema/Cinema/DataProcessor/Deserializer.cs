namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2:f2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            moviesJsonDto[] moviesDto = JsonConvert.DeserializeObject<moviesJsonDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Movie> moviesToBeAdded = new List<Movie>();
            HashSet<string> titlesInBase = context.Movies.Select(x => x.Title).ToHashSet();

            foreach (var dto in moviesDto)
            {
                try
                {
                    var newMovie = Mapper.Map<Movie>(dto);
                    if (titlesInBase.Any(x => x == newMovie.Title))
                    {
                        throw new InvalidOperationException("MovieTitle already added");
                    }
                    if (!AttributeClassValidator.IsValid(newMovie))
                    {
                        throw new Exception("Movie Parameters not valid");
                    }
                    titlesInBase.Add(newMovie.Title);
                    moviesToBeAdded.Add(newMovie);
                    sb.AppendLine(string.Format(SuccessfulImportMovie, newMovie.Title, newMovie.Genre.ToString(), newMovie.Rating));
                }
                catch (Exception)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Movies.AddRange(moviesToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            hallJsonDto[] hallsDto = JsonConvert.DeserializeObject<hallJsonDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Hall> HallsToBeAdded = new List<Hall>();
            HashSet<string> hallNamesInBase = context.Halls.Select(x => x.Name).ToHashSet();

            foreach (var dto in hallsDto)
            {
                try
                {

                    if (hallNamesInBase.Contains(dto.Name))
                    {
                        throw new InvalidOperationException("HallName already added!");
                    }
                    if (dto.SeatCount <= 0)
                    {
                        throw new InvalidOperationException("Non Positive SeatCount!");
                    }
                    var newHall = Mapper.Map<Hall>(dto);

                    if (!AttributeClassValidator.IsValid(newHall))
                    {
                        throw new Exception("Movie Parameters not valid");
                    }

                    for (int i = 0; i < dto.SeatCount; i++)
                    {
                        newHall.Seats.Add(new Seat());
                    }

                    hallNamesInBase.Add(newHall.Name);
                    HallsToBeAdded.Add(newHall);
                    string projectionType = "Normal";
                    if (dto.Is3D && !dto.Is4Dx) projectionType = "3D";
                    if (!dto.Is3D && dto.Is4Dx) projectionType = "4Dx";
                    if (dto.Is3D && dto.Is4Dx) projectionType = "3D/4Dx";

                    sb.AppendLine(string.Format(SuccessfulImportHallSeat, newHall.Name, projectionType, dto.SeatCount));
                }
                catch (Exception)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Halls.AddRange(HallsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(projectionXmlDto[]), new XmlRootAttribute("Projections"));
            StringBuilder sb = new StringBuilder();

            Dictionary<int, string> movie_Id_Names_Available = context.Movies.Select(x => new { x.Id, x.Title }).ToDictionary(x => x.Id, x => x.Title);

            HashSet<int> hallIdsAvailable = context.Halls.Select(x => x.Id).ToHashSet();
            projectionXmlDto[] projectsDtos = (projectionXmlDto[])serializer.Deserialize(new StringReader(xmlString));

            Stack<Projection> projectionsToBeAdded = new Stack<Projection>();
            foreach (projectionXmlDto dto in projectsDtos)
            {
                try
                {
                    if (!hallIdsAvailable.Contains(dto.HallId)) throw new InvalidOperationException("Invalid HallId");
                    if (!movie_Id_Names_Available.ContainsKey(dto.MovieId)) throw new InvalidOperationException("Invalid MovieId");
                    var newProjection = Mapper.Map<Projection>(dto);

                    if (!AttributeClassValidator.IsValid(newProjection)) throw new InvalidOperationException("Invalid input Data");

                    projectionsToBeAdded.Push(newProjection);
                    string projectionDate = newProjection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    sb.AppendLine(string.Format(SuccessfulImportProjection, movie_Id_Names_Available[dto.MovieId], projectionDate));

                }
                catch (Exception ex)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Projections.AddRange(projectionsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(cutomerXmlDto[]), new XmlRootAttribute("Customers"));
            StringBuilder sb = new StringBuilder();

            var customersDtos = (cutomerXmlDto[])serializer.Deserialize(new StringReader(xmlString));

            var customersToBeAdded = new Stack<Customer>();
            foreach (var dto in customersDtos)
            {
                try
                {
                    var newCustomer = Mapper.Map<Customer>(dto);
                    if (!AttributeClassValidator.IsValid(newCustomer)
                      ||   !newCustomer.Tickets.All(x => AttributeClassValidator.IsValid(x)))
                    {
                        throw new InvalidOperationException("Invalid data input");
                    }
                    //TODO check if one user has more than one ticket with same projectionId-NP

                      sb.AppendLine(string.Format(SuccessfulImportCustomerTicket
                        ,newCustomer.FirstName
                        ,newCustomer.LastName
                        ,newCustomer.Tickets.Count));
                    customersToBeAdded.Push(newCustomer);
                }
                catch (Exception ex)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Customers.AddRange(customersToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
    }
}