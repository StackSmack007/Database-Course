using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using SoftJail;
using SoftJail.Data;
using SoftJail.Data.Models;
using SoftJail.DataProcessor;


[TestFixture]
public class Export_000_002
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("SoftJailProfile")));

        this.serviceProvider = ConfigureServices<SoftJailDbContext>("SoftJail");
    }

    [Test]
    public void ExportPrisonersInboxZeroTest1()
    {
        var context = serviceProvider.GetService<SoftJailDbContext>();

        SeedDatabase(context);

        var expectedXml = "<Prisoners><Prisoner><Id>3</Id><Name>Binni Cornhill</Name><IncarcerationDate>1967-01-29</IncarcerationDate><EncryptedMessages><Message><Description>!?sdnasuoht evif-ytnewt rof deksa uoy ro orez artxe na ereht sI</Description></Message></EncryptedMessages></Prisoner><Prisoner><Id>2</Id><Name>Diana Ebbs</Name><IncarcerationDate>1963-01-21</IncarcerationDate><EncryptedMessages><Message><Description>.kcab draeh ton evah llits I dna  ,skeew 2 tuoba ni si esaeler mubla ehT .dnuoranrut rof skeew 6-4 sekat ynapmoc DC eht dias yllanigiro eH .gnitiaw llits ma I</Description></Message><Message><Description>.emit ruoy ekat ot uoy ekil lliw ew dna krow ruoy ekil I .hsur on emit ruoy ekat ,enif si tahT</Description></Message></EncryptedMessages></Prisoner><Prisoner><Id>1</Id><Name>Melanie Simonich</Name><IncarcerationDate>1957-01-29</IncarcerationDate><EncryptedMessages><Message><Description>krowten nIdekniL ruoy ot em dda esaelp</Description></Message><Message><Description>!uoy rof ecalp tseb eht dnuof uoy epoh i einaleM</Description></Message><Message><Description>.edulcni ot ton detpo yeht hcihw tub ecalp tsrif eht ni dedeen yeht meht dlot I sgniht lla - cte ,sppa lufesu ,tnetnoc cimanyd evisnopser ylluf ekil sgniht tnemelpmi ot detnaw yeht tuo snruT</Description></Message></EncryptedMessages></Prisoner></Prisoners>";
        var expectedOutput = XDocument.Parse(expectedXml);
        var actualXml = Serializer.ExportPrisonersInbox(context, "Melanie Simonich,Diana Ebbs,Binni Cornhill");

        var actualOutput = XDocument.Parse(actualXml);

        var expectedOutputXml = expectedOutput.ToString(SaveOptions.DisableFormatting);
        var actualOutputXml = actualOutput.ToString(SaveOptions.DisableFormatting);
        Assert.That(actualOutputXml, Is.EqualTo(expectedOutputXml).NoClip,
            $"{nameof(Serializer.ExportPrisonersInbox)} output is incorrect!");
    }

    private static void SeedDatabase(SoftJailDbContext context)
    {
        const string jsonSeedData = "[{'Id':1,'FullName':'Melanie Simonich','IncarcerationDate':'1957-01-29T00:03:00','Mails':[{'Id':1,'Sender':'Zonda Vasiljevic','Description':'please add me to your LinkedIn network'},{'Id':26,'Sender':'Shell Lofthouse','Description':'Melanie i hope you found the best place for you!'},{'Id':27,'Sender':'My Ansell','Description':'Turns out they wanted to implement things like fully responsive dynamic content, useful apps, etc - all things I told them they needed in the first place but which they opted not to include.'}]},{'Id':2,'FullName':'Diana Ebbs','IncarcerationDate':'1963-01-21T00:08:00','Mails':[{'Id':18,'Sender':'Beale Jackman','Description':'I am still waiting. He originally said the CD company takes 4-6 weeks for turnaround. The album release is in about 2 weeks,  and I still have not heard back.'},{'Id':19,'Sender':'Lillian MacCrackan','Description':'That is fine, take your time no rush. I like your work and we will like you to take your time.'}]},{'Id':3,'FullName':'Binni Cornhill','IncarcerationDate':'1967-01-29T00:04:00','Mails':[{'Id':17,'Sender':'Aindrea Harniman','Description':'Is there an extra zero or you asked for twenty-five thousands?!'}]},{'Id':4,'FullName':'Jenny Rhys','IncarcerationDate':'1973-01-17T00:10:00','Mails':[{'Id':15,'Sender':'Hamil Mathison','Description':'Yeah, we prefer to stick to company templates.'},{'Id':16,'Sender':'Beale Jackman','Description':'Well, I am a designer, not a writer. I am just telling you what I need.'}]},{'Id':5,'FullName':'Ellette Lante','IncarcerationDate':'1979-01-23T00:05:00','Mails':[{'Id':13,'Sender':'Kylie Caldayrou','Description':'What is this? We were hoping for something more streamlined and professional.'},{'Id':14,'Sender':'Aindrea Harniman','Description':'That sounds like an interesting project! What is your budget?'},{'Id':22,'Sender':'Branden Campagne','Description':'I am trying to offer you a job, but if this is how you do business:'}]},{'Id':6,'FullName':'Verena Fidoe','IncarcerationDate':'1963-01-08T00:03:00','Mails':[{'Id':9,'Sender':'Elyssa Largan','Description':'The next day, my husband got the identical request from her in his FB inbox, giving him TWO days. Not surprisingly, he did not even respond.'},{'Id':10,'Sender':'Leona Horribine','Description':'Before we talk about any of that, how much will your services cost?'},{'Id':12,'Sender':'Sidney O Neary','Description':'Looks great, but only received one copy of the flyer! I had said that I would like to print 250 of them'}]},{'Id':7,'FullName':'Rosanna Lissaman','IncarcerationDate':'1964-01-29T00:11:00','Mails':[{'Id':7,'Sender':'Christel Peffer','Description':'OK, what do you need to be done? And when?'},{'Id':8,'Sender':'Kimmi Kilian','Description':'Some animations and pictures and things.'}]},{'Id':8,'FullName':'Isis Gasticke','IncarcerationDate':'1963-01-04T00:11:00','Mails':[{'Id':4,'Sender':'Ulrikaumeko Huson','Description':'I followed up a few times. Eventually, he texted me to let me know that he had gotten my email but had not had time to respond.'},{'Id':5,'Sender':'Hedwiga Swainger','Description':'I sent the list. After a conversation with the other camera operator, it seemed like she knew what to do, so I stepped aside to let her work.'},{'Id':6,'Sender':'Mikey Bothbie','Description':' I mean, sure, but it will be pretty busy. I really recommend you keep it simple for the screen.'}]},{'Id':9,'FullName':'Alys Dearl','IncarcerationDate':'1978-01-09T00:09:00','Mails':[{'Id':20,'Sender':'Randie Pomfrey','Description':'What if I find his head from another photo pointing in the right direction? '}]},{'Id':10,'FullName':'Maury Pelcheur','IncarcerationDate':'1970-01-01T00:03:00','Mails':[{'Id':2,'Sender':'Isabelita Flott','Description':'What do we do about the logo? It is not masculine enough.'},{'Id':3,'Sender':'Ulrikaumeko Huson','Description':'I am sorry you feel that way. But can you tell me what you SPECIFICALLY like about these examples?'}]},{'Id':11,'FullName':'Boycie Castellaccio','IncarcerationDate':'1977-01-19T00:02:00','Mails':[{'Id':24,'Sender':'Malory Duker','Description':'A client who I had worked with for some years came back to me after an initial draft with a long list of changes.'},{'Id':25,'Sender':'Quintina Matthews','Description':'What is taking so long with my sign? It is been a few weeks since I ordered it, and I still did not see any designs yet.'},{'Id':35,'Sender':'Conroy Braycotton','Description':' Well, sure. It wont be black anymore though. Lightening it even slightly will make it a dark grey/charcoal.'}]},{'Id':12,'FullName':'Garland Swepstone','IncarcerationDate':'1966-01-10T00:03:00','Mails':[{'Id':44,'Sender':'Kori Bayless','Description':'The client does not respond to my reply. Two days later (!) the text arrives. '},{'Id':45,'Sender':'Francklyn Rickford','Description':'Thanks for your request, we can definitely help you with that and there is plenty of time to get it done. '}]},{'Id':13,'FullName':'Fiona Mattecot','IncarcerationDate':'1967-01-25T00:11:00','Mails':[{'Id':38,'Sender':'Adams Mewis','Description':'The past couple of years, our agency has done a brochure for a company that then takes the files and changes out pictures, colors and text for different target audiences.'},{'Id':39,'Sender':'Faythe Laroux','Description':'Based on all the files, I am not sure this was sent correctly because it was our understanding that all the links would be able to be swapped out and these files are just layered PSDs.'},{'Id':40,'Sender':'Vito Maleham','Description':'I work at a small company in a position unrelated to any design discipline but have a strong background and a good eye for design.'},{'Id':41,'Sender':'Letty Bon','Description':'There were flowers on the home page and no one asked what those meant.'},{'Id':42,'Sender':'Tyrone Balm','Description':' I have asked you for a due date several times and you still have not provided one. What is the deadline?'},{'Id':43,'Sender':'Shandee Orsi','Description':'Yes! Just fix it.'}]},{'Id':14,'FullName':'Lenette Jorn','IncarcerationDate':'1970-01-22T00:12:00','Mails':[{'Id':34,'Sender':'Elisha Downey','Description':'Nothing is working, we came in this morning and it is all wonky!'},{'Id':36,'Sender':'Briant Sperrett','Description':'I am a developer, so I understand how this works.'},{'Id':37,'Sender':'Jaquenette Corran','Description':'I am currently working with a regular client who sends me work to ghostwrite five days a week. Occasionally, he is a bit unusual with his requests. '},{'Id':46,'Sender':'Brennan Patters','Description':'My client sent me two files and asked me to overlay one on top of the other. One was the border of our state and the other was cursive lowercase text. Both were flat bitmaps.'}]},{'Id':15,'FullName':'Aguistin Rawls','IncarcerationDate':'1955-01-30T00:08:00','Mails':[{'Id':33,'Sender':'Dynah Lawee','Description':'The file she sent me was another file, to a different song, still in an incredibly high key. I sent her a sample, to which she unexpectedly approved. I recorded the entire song and sent it to her.'}]},{'Id':16,'FullName':'Benji Ballefant','IncarcerationDate':'1967-01-12T00:09:00','Mails':[{'Id':31,'Sender':'Leona Cutford','Description':'I don not know man, it just looks too wide.'},{'Id':32,'Sender':'Augustine Eickhoff','Description':'Is there a way you can make your voice sound: more like an adult?'}]},{'Id':17,'FullName':'Rosmunda Yoodall','IncarcerationDate':'1965-01-18T00:05:00','Mails':[{'Id':28,'Sender':'Billye Hakey','Description':'The Images that were sent were old posters when it was a club: lens flare, dancing girls, table service:'},{'Id':29,'Sender':'Tanya Ligertwood','Description':'Okay no problem. We just wanna get them in the mail hopefully today. Email me when you are home.'},{'Id':30,'Sender':'El Done','Description':'I am so frustrated with our website backend and the complete lack of design flexibility it affords. I want our pages to be DYNAMIC! Our PRINTED material (attached) is WAY more dynamic than our web page. How can this be? I would love any insights you have.'}]},{'Id':18,'FullName':'Dieter MacFadzan','IncarcerationDate':'1959-01-28T00:04:00','Mails':[{'Id':11,'Sender':'Joannes Seckom','Description':'Six months later, I got an email asking for printable 1/6 scale keys and the loop started over.'}]},{'Id':19,'FullName':'Audrie Billion','IncarcerationDate':'1964-01-22T00:05:00','Mails':[{'Id':21,'Sender':'Mikey Bothbie','Description':'So here is the code. This will make it really easy to update our data.'},{'Id':23,'Sender':'Christel Peffer','Description':'No need to make the text easier to read. The text is hard to understand in itself.'},{'Id':47,'Sender':'Kimmi Kilian','Description':'You know: (techno) Like The Eagles!'}]}]";
        var prisoners = JsonConvert.DeserializeObject<Prisoner[]>(jsonSeedData);
        context.Prisoners.AddRange(prisoners);
        context.SaveChanges();
    }
    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<TContext>(
                options => options
                    .UseInMemoryDatabase(databaseName)
                    .UseLazyLoadingProxies(useLazyLoading)
            );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }
}