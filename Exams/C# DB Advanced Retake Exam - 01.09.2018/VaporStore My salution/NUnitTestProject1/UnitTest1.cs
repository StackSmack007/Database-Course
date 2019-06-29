using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using VaporStore;
using VaporStore.Data;
using VaporStore.Data.Models;
using VaporStore.DataProcessor;

[TestFixture]
public class Import_000_003
{
    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("VaporStoreProfile")));

        this.serviceProvider = ConfigureServices<VaporStoreDbContext>("VaporStore");
    }

    [Test]
    public void ImportPurchasesZeroTest()
    {
        var context = this.serviceProvider.GetService<VaporStoreDbContext>();

        SeedDatabase(context);

        var inputXml =
            "<Purchases><Purchase title=\"Dungeon Warfare 2\"><Type>Digital</Type><Key>ZTZ3-0D2S-G4TJ</Key><Card>1833 5024 0553 6211</Card><Date>07/12/2016 05:49</Date></Purchase><Purchase title=\"The Crew 2\"><Type>Retail</Type><Key>DCU0-S60G-NTQJ</Key><Card>5208 8381 5687 8508</Card><Date>22/01/2017 09:33</Date></Purchase><Purchase title=\"Slay the Spire\"><Type>Digital</Type><Key>KIJH-7JG6-0BHP</Key><Card>5208 8381 5687 8508</Card><Date>11/01/2018 19:46</Date></Purchase></Purchases>";

        var actualOutput =
            Deserializer.ImportPurchases(context, inputXml).TrimEnd();
        var expectedOutput =
            "Imported Dungeon Warfare 2 for lsilbert\r\nImported The Crew 2 for aruthven\r\nImported Slay the Spire for aruthven";

        var assertContext = this.serviceProvider.GetService<VaporStoreDbContext>();

        var expectedPurchasesCount = 3;
        var actualPurchasesCount = assertContext.Purchases.Count();

        Assert.That(actualPurchasesCount, Is.EqualTo(expectedPurchasesCount),
            $"Inserted {nameof(context.Purchases)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Deserializer.ImportPurchases)} output is incorrect!");
    }

    private static void SeedDatabase(VaporStoreDbContext context)
    {
        var gamesJson = "[{\"Id\":2,\"Name\":\"Factorio\"},{\"Id\":3,\"Name\":\"Yu-Gi-Oh! Duel Links\"},{\"Id\":4,\"Name\":\"Trove\"},{\"Id\":6,\"Name\":\"Wreckfest\"},{\"Id\":7,\"Name\":\"Terraria\"},{\"Id\":9,\"Name\":\"NBA 2K18\"},{\"Id\":11,\"Name\":\"Total War: WARHAMMER II\"},{\"Id\":12,\"Name\":\"Garry's Mod\"},{\"Id\":13,\"Name\":\"Rust\"},{\"Id\":17,\"Name\":\"Slay the Spire\"},{\"Id\":19,\"Name\":\"Bridge Constructor Portal\"},{\"Id\":20,\"Name\":\"Pro Cycling Manager 2018\"},{\"Id\":21,\"Name\":\"Dungeon Warfare 2\"},{\"Id\":22,\"Name\":\"Soul at Stake\"},{\"Id\":24,\"Name\":\"Tom Clancy's Ghost Recon Wildlands\"},{\"Id\":25,\"Name\":\"Neverwinter\"},{\"Id\":26,\"Name\":\"Squad\"},{\"Id\":27,\"Name\":\"Vampyr\"},{\"Id\":28,\"Name\":\"Brawlhalla\"},{\"Id\":29,\"Name\":\"Conan Exiles\"},{\"Id\":30,\"Name\":\"Call of Duty: Black Ops III\"},{\"Id\":31,\"Name\":\"Human: Fall Flat\"},{\"Id\":32,\"Name\":\"Guns, Gore and Cannoli 2\"},{\"Id\":33,\"Name\":\"DARK SOULS: REMASTERED\"},{\"Id\":34,\"Name\":\"The Elder Scrolls V: Skyrim Special Edition\"},{\"Id\":36,\"Name\":\"FINAL FANTASY XIV Online\"},{\"Id\":38,\"Name\":\"Warface\"},{\"Id\":39,\"Name\":\"NARUTO SHIPPUDEN: Ultimate Ninja STORM 4\"},{\"Id\":40,\"Name\":\"Black Desert Online\"},{\"Id\":41,\"Name\":\"Warhammer 40,000: Gladius - Relics of War\"},{\"Id\":42,\"Name\":\"Planet Coaster\"},{\"Id\":45,\"Name\":\"Rocket League\"},{\"Id\":46,\"Name\":\"Far Cry 5\"},{\"Id\":47,\"Name\":\"FOR HONOR\"},{\"Id\":48,\"Name\":\"Tom Clancy's Rainbow Six Siege\"},{\"Id\":49,\"Name\":\"Warframe\"},{\"Id\":51,\"Name\":\"MONSTER HUNTER: WORLD\"},{\"Id\":52,\"Name\":\"Team Fortress 2\"},{\"Id\":53,\"Name\":\"Counter-Strike: Global Offensive\"},{\"Id\":54,\"Name\":\"Path of Exile\"},{\"Id\":55,\"Name\":\"Rules Of Survival\"},{\"Id\":56,\"Name\":\"Banished\"},{\"Id\":59,\"Name\":\"Divinity: Original Sin 2\"},{\"Id\":61,\"Name\":\"Shadowverse CCG\"},{\"Id\":62,\"Name\":\"RimWorld\"},{\"Id\":63,\"Name\":\"Crash Bandicoot N. Sane Trilogy\"},{\"Id\":64,\"Name\":\"SMITE\"},{\"Id\":65,\"Name\":\"Paladins\"},{\"Id\":67,\"Name\":\"Stardew Valley\"},{\"Id\":68,\"Name\":\"House Flipper\"},{\"Id\":70,\"Name\":\"The Crew 2\"},{\"Id\":71,\"Name\":\"Dead by Daylight\"},{\"Id\":72,\"Name\":\"Football Manager 2018\"},{\"Id\":73,\"Name\":\"Raft\"},{\"Id\":74,\"Name\":\"The Witcher 3: Wild Hunt\"}]";
        var games = JsonConvert.DeserializeObject<Game[]>(gamesJson);

        var usersJson = "[{\"Id\":1,\"Username\":\"lsilbert\",\"Cards\":[{\"Id\":1,\"Number\":\"1833 5024 0553 6211\"},{\"Id\":33,\"Number\":\"5625 0434 5999 6254\"},{\"Id\":34,\"Number\":\"4902 6975 5076 5316\"}]},{\"Id\":2,\"Username\":\"kmertgen\",\"Cards\":[{\"Id\":27,\"Number\":\"1268 2352 8506 0500\"}]},{\"Id\":3,\"Username\":\"svannini\",\"Cards\":[{\"Id\":24,\"Number\":\"8928 2433 2516 9511\"},{\"Id\":25,\"Number\":\"2175 1623 6855 0876\"},{\"Id\":26,\"Number\":\"1642 7380 2920 7598\"}]},{\"Id\":4,\"Username\":\"aputland\",\"Cards\":[{\"Id\":21,\"Number\":\"2263 5851 7894 9441\"},{\"Id\":22,\"Number\":\"7660 9400 3206 5606\"},{\"Id\":23,\"Number\":\"9454 1480 3127 1373\"}]},{\"Id\":5,\"Username\":\"vsjollema\",\"Cards\":[{\"Id\":18,\"Number\":\"7815 5830 0145 0448\"},{\"Id\":19,\"Number\":\"8608 6806 8238 3092\"},{\"Id\":20,\"Number\":\"4846 1275 4235 3039\"}]},{\"Id\":6,\"Username\":\"rabbison\",\"Cards\":[{\"Id\":17,\"Number\":\"5747 3965 9959 7596\"}]},{\"Id\":7,\"Username\":\"bgunston\",\"Cards\":[{\"Id\":14,\"Number\":\"4347 6119 2799 9266\"},{\"Id\":15,\"Number\":\"3762 5646 9250 3278\"},{\"Id\":16,\"Number\":\"9329 2624 0151 4535\"}]},{\"Id\":8,\"Username\":\"bgraith\",\"Cards\":[{\"Id\":12,\"Number\":\"5962 2881 2375 4209\"},{\"Id\":13,\"Number\":\"4611 7969 4921 6749\"}]},{\"Id\":9,\"Username\":\"mgillicuddy\",\"Cards\":[{\"Id\":10,\"Number\":\"4499 3123 4695 9542\"},{\"Id\":11,\"Number\":\"9924 7778 1587 0277\"}]},{\"Id\":10,\"Username\":\"ehellard\",\"Cards\":[{\"Id\":7,\"Number\":\"2643 8516 1644 3240\"},{\"Id\":8,\"Number\":\"3013 7441 5769 1224\"},{\"Id\":9,\"Number\":\"3563 6747 1527 9955\"}]},{\"Id\":11,\"Username\":\"mdickson\",\"Cards\":[{\"Id\":4,\"Number\":\"5317 5177 3653 0084\"},{\"Id\":5,\"Number\":\"0100 5244 4964 3100\"},{\"Id\":6,\"Number\":\"1478 0420 6326 7013\"}]},{\"Id\":12,\"Username\":\"bfrontczak\",\"Cards\":[{\"Id\":2,\"Number\":\"3962 3870 5087 4536\"},{\"Id\":3,\"Number\":\"9104 6735 7127 3894\"}]},{\"Id\":13,\"Username\":\"mgraveson\",\"Cards\":[{\"Id\":29,\"Number\":\"7991 7779 5123 9211\"},{\"Id\":31,\"Number\":\"7790 7962 4262 5606\"}]},{\"Id\":14,\"Username\":\"fstoter\",\"Cards\":[{\"Id\":60,\"Number\":\"9466 9592 0503 1368\"}]},{\"Id\":15,\"Username\":\"bcorcut\",\"Cards\":[{\"Id\":32,\"Number\":\"7460 6498 2791 0231\"}]},{\"Id\":16,\"Username\":\"asikorsky\",\"Cards\":[{\"Id\":58,\"Number\":\"8746 7253 1464 1729\"},{\"Id\":59,\"Number\":\"3863 8683 6862 0373\"}]},{\"Id\":17,\"Username\":\"klife\",\"Cards\":[{\"Id\":56,\"Number\":\"0540 4834 3653 5943\"},{\"Id\":57,\"Number\":\"2962 0872 0998 4724\"}]},{\"Id\":18,\"Username\":\"gsmallthwaite\",\"Cards\":[{\"Id\":53,\"Number\":\"9889 9719 6896 7474\"},{\"Id\":54,\"Number\":\"5994 2396 1516 2411\"},{\"Id\":55,\"Number\":\"0274 2943 4672 4028\"}]},{\"Id\":19,\"Username\":\"kroderighi\",\"Cards\":[{\"Id\":52,\"Number\":\"7036 3344 0149 7880\"}]},{\"Id\":20,\"Username\":\"hrichardson\",\"Cards\":[{\"Id\":50,\"Number\":\"4082 9960 2674 5955\"},{\"Id\":51,\"Number\":\"5811 6621 2962 1020\"}]},{\"Id\":21,\"Username\":\"atobin\",\"Cards\":[{\"Id\":48,\"Number\":\"6975 1775 3435 4897\"},{\"Id\":49,\"Number\":\"9039 3485 8754 8863\"}]},{\"Id\":22,\"Username\":\"fdedam\",\"Cards\":[{\"Id\":46,\"Number\":\"9574 1800 3833 2972\"},{\"Id\":47,\"Number\":\"9976 1161 9586 1806\"}]},{\"Id\":23,\"Username\":\"cbelchamber\",\"Cards\":[{\"Id\":44,\"Number\":\"1576 2740 8903 1499\"},{\"Id\":45,\"Number\":\"6842 0546 4406 5606\"}]},{\"Id\":24,\"Username\":\"kcarroll\",\"Cards\":[{\"Id\":41,\"Number\":\"2844 3311 3796 4444\"},{\"Id\":42,\"Number\":\"7716 6230 0769 9366\"},{\"Id\":43,\"Number\":\"2829 0002 6052 6217\"}]},{\"Id\":25,\"Username\":\"wskep\",\"Cards\":[{\"Id\":40,\"Number\":\"0327 7877 3023 9451\"}]},{\"Id\":26,\"Username\":\"nparadyce\",\"Cards\":[{\"Id\":38,\"Number\":\"0798 3871 2521 2016\"},{\"Id\":39,\"Number\":\"1661 2121 6244 8487\"}]},{\"Id\":27,\"Username\":\"dsharple\",\"Cards\":[{\"Id\":36,\"Number\":\"5345 7357 8866 7508\"},{\"Id\":37,\"Number\":\"6752 6869 9870 9732\"}]},{\"Id\":28,\"Username\":\"aruthven\",\"Cards\":[{\"Id\":35,\"Number\":\"5208 8381 5687 8508\"}]},{\"Id\":29,\"Username\":\"cgara\",\"Cards\":[{\"Id\":28,\"Number\":\"5103 9356 9768 6854\"}]},{\"Id\":30,\"Username\":\"spaprotny\",\"Cards\":[{\"Id\":30,\"Number\":\"9185 2070 3009 4543\"},{\"Id\":61,\"Number\":\"6777 2480 1837 5824\"}]}]";
        var users = JsonConvert.DeserializeObject<User[]>(usersJson);

        context.AddRange(games);
        context.AddRange(users);
        context.SaveChanges();
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
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
}