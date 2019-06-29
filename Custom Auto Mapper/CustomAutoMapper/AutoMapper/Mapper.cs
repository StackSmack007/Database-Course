using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
    public static class Mapper
    {

        private static readonly Type[] allowedTypes =
            {
              typeof(string),
              typeof(char),
              typeof(int),
              typeof(long),
              typeof(float),
              typeof(short),
              typeof(double),
              typeof(decimal),
              typeof(bool),
              typeof(DateTime)
             };

        public static T Map<T>(object source)
        {

            if (source is null)
            {
                throw new ArgumentException("NullObject cant be matched!");
            }

            Type typeOfresult = typeof(T);

            T emptyInstance = (T)Activator.CreateInstance(typeOfresult);

            return DoMap(source, emptyInstance);
        }

        private static T DoMap<T>(object source, T destination)
        {
            Type sourceType = source.GetType();
            PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            Dictionary<string, Type> sourcePropNamesTypes = sourceProperties.ToDictionary(x => x.Name, x => x.PropertyType);

            Type destinationType = typeof(T);
            PropertyInfo[] destinationProperties = destinationType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in destinationProperties.Where(x=>x.CanWrite))
            {
                string propertyName = property.Name;
                var sourceValue = sourceProperties.FirstOrDefault(x => x.Name == propertyName)?.GetValue(source);

                Type propertyType = property.PropertyType;
                //Case : Names mismatch=> Default value for required property!
                if (!sourcePropNamesTypes.ContainsKey(propertyName))
                {
                    SetNullOrDefault(property, destination);
                    continue;
                }
                //Case : Type is one of the allowed ones => Mapping occures!
                if (allowedTypes.Contains(propertyType) || propertyType == sourcePropNamesTypes[propertyName])
                {
                    property.SetValue(destination, sourceValue);
                    continue;
                }
                //Case :Names match, Types missmatch and destinationType is not collection => inner maching occures! 
                if (sourcePropNamesTypes[propertyName] != propertyType &&
                    !IsCollection(destination, propertyName) && !IsCollection(source, propertyName))
                {

                    var mappedValue = AddAndMatch(sourceValue, destination, sourceProperties, propertyType);
                    property.SetValue(destination, mappedValue);
                }
                //Case :Collection of things that need matching complicated Objects!
                if (IsCollection(destination, propertyName) && IsCollection(source, propertyName))
                {
                    var elementType = propertyType.GetGenericArguments().Single();
                    foreach (var srcV in (IEnumerable<object>)sourceValue)
                    {
                        var mappedValue = AddAndMatch(srcV, destination, sourceProperties, elementType);
                        ((IList)property.GetValue(destination)).Add(mappedValue);
                       // propertyType.GetMethod("Add").Invoke(property.GetValue(destination), new[] { mappedValue });
                    }
                }
            }

            return destination;
        }

        private static bool IsCollection(object item, string nameOfProperty)
        {
            var sourcePropertyValue = item.GetType().GetProperty(nameOfProperty).PropertyType;

            return sourcePropertyValue.GetInterfaces().Any(x => x.Name.Contains("IEnumerable"));
        }

        private static object AddAndMatch<T>(object sourceValue, T destination, PropertyInfo[] sourceProperties, Type propertyType)
        {
            var mapMethod = Assembly.GetCallingAssembly()
                 .GetTypes().FirstOrDefault(x => x.Name == "Mapper")
                 .GetMethods(BindingFlags.Static | BindingFlags.Public)
                 .FirstOrDefault(x => x.Name == "Map").MakeGenericMethod(propertyType);

            return mapMethod.Invoke(null, new object[] { sourceValue });


        }

        private static void SetNullOrDefault(PropertyInfo property, object instance)
        {
            property.SetValue(instance, property.PropertyType.IsValueType
                                           ? Activator.CreateInstance(property.PropertyType)
                                           : null);
        }
    }
}