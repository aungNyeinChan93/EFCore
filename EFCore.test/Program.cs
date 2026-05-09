using Azure.Core.Serialization;
using EFCore.data.Data;
using EFCore.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


var context = new FootballDbContext();

async Task AddData()
{
    var newLeague = new League { Name = "Premier League" };
    var newTeam = new Team { Name = "Manchester United ",League = newLeague };
    await context.AddAsync(newTeam);
    await context.SaveChangesAsync();
}

async Task MultipleAddTeam()
{
    var teams = new List<Team>
    {
        new Team {Name = "Arsenal",LeagueId = 1},
        new Team {Name = "Manchester City",LeagueId = 1},
        new Team {Name = "Liverpool",LeagueId = 1},
        new Team {Name = "AFC Bournemouth",LeagueId = 1},
        new Team {Name = "Aston Villa",LeagueId = 1},
        new Team {Name = "Brighton",LeagueId = 1},
        new Team {Name = "Nottingham Forest",LeagueId = 1},
        new Team {Name = "Newcastle United",LeagueId = 1},
        new Team {Name = "Brentford",LeagueId = 1},
        new Team {Name = "Chelsea",LeagueId = 1},
        new Team {Name = "Everton",LeagueId = 1},
        new Team {Name = "Fulham",LeagueId = 1},
        new Team {Name = "Crystal Palace",LeagueId = 1},
        new Team {Name = "Leeds United",LeagueId = 1},
        new Team {Name = "Tottenham Hotspur",LeagueId = 1},
        new Team {Name = "West Ham United",LeagueId = 1},
        new Team {Name = "Sunderland",LeagueId = 1},
        new Team {Name = "Wolves",LeagueId = 1},
        new Team {Name = "Stoke City ",LeagueId = 1},
    };

    await context.Teams.AddRangeAsync(teams);
    await context.SaveChangesAsync();
    Console.WriteLine("teams added!");
}

async Task ALlTeams()
{

    var teams = await context.Teams
        //.Include(t => t.Palyers)
        .Include(t=> t.League)
        .Select(t=> new
        {
            teamName = t.Name,
            leagueName = t.League!.Name
        })
        .AsNoTracking().ToListAsync();

    foreach (var team in teams)
    {
        //Console.WriteLine(team.Name);
        //Console.WriteLine(team.League?.Name);
        Console.WriteLine(JsonSerializer.Serialize(team, new JsonSerializerOptions { WriteIndented = true }));
    }
}


async Task FilterTeam()
{
    //var teams = await context.Teams
    //    .Include(t => t.League)
    //    .Where(q => q.League!.Name != "Arsenal")
    //    .ToListAsync();

    var query = context.Leagues
        .Include(l => l.Teams)
        .Select(t => t.Teams)
        .AsQueryable();

    var teams =await query.ToListAsync();
    var select = teams[0]!
        .Where(t => t.Name.Contains("Man"))
        //.Where(t => EF.Functions.Like(t.Name,"%Man%"))
        .FirstOrDefault(t=> true,new Team() { Name = "default"});

    var league = await context.Leagues.FindAsync(keyValues:1); 
    Console.WriteLine(JsonSerializer.Serialize(league, new JsonSerializerOptions() { WriteIndented= true}));
}


void LinqSelect()
{
    var query = from i in context.Teams
                    where i.Name == "Man"
                   select i.Name;
    //var teamName2 = (from i in context.Teams select i.Name).ToList<Team>() as List<Team>;

    var teamLists = query.ToList();

    var t = (from i in teamLists select i.ToLower()).ToList();

    //var res = teamName.ToList().GroupBy(t=>t.,);
    //var team = teamName.Single<Team>();
    //Console.WriteLine(JsonSerializer.Serialize(t,new JsonSerializerOptions() { WriteIndented =true}));
    Console.WriteLine(JsonSerializer.Serialize(query.ToList(),new JsonSerializerOptions() { WriteIndented = true}));
}


async Task AddLeagues()
{
    var leagues = new List<League>
    {
        new League{Name = "LaLiga"},
        new League{Name = "Serie A"},
        new League{Name = "Bundesliga"},
        new League{Name = "Ligue 1"},
        new League{Name = "Eredivisie"},
        new League{Name = "Liga Portugal"},
    };

    await context.Leagues.AddRangeAsync(leagues);
    await context.SaveChangesAsync();

}

async Task LeagueUpdate()
{
    //var league = new League { Name = "update 2" ,LeagueId = 8};
    var league = new League { Name = "update 2"};
    context.Leagues.Update(league);  // update -> find id -> if true -> update -> if false -> create
    await context.SaveChangesAsync();
    Console.WriteLine("Update league!");
}

async Task DeleteLeague(int id)
{
    var league = await context.Leagues.FindAsync(id);
    context.Leagues.Remove(league!);
    await context.SaveChangesAsync();
    Console.WriteLine("Delete success!");
}

async Task DeleteLeagues()
{
    var leagues = await context.Leagues.Where(l=> l.LeagueId >=9).ToListAsync();
    context.Leagues.RemoveRange(leagues);
    await context.SaveChangesAsync();
    Console.WriteLine("Delete success!");
}


async Task GetAllLeagues()
{
    var leagues = await context.Leagues
        .AsNoTracking()
        .Include(x => x.Teams)
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(leagues,new JsonSerializerOptions() { WriteIndented =true}));
}


async Task GetAllPayers()
{
    var players = await context.Players.AsNoTracking()
        .Include(p => p.Team)
        .Select(p=> new
        {
            PlayerInfo = new Player { PalyerId = p.PalyerId, Name = p.Name, Age = p.Age, Country = p.Country, AssistScore = p.AssistScore, GoalScore = p.GoalScore },
            Team = p.Team!.Name

        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(players,new JsonSerializerOptions() { WriteIndented = true}));
}

async Task GetAllUsers()
{
    var users = await context.Users.AsNoTracking()
                .Include(u => u.UserDetail)
                .Include(u=>u.School)
                .Select(u=> new
                {
                    user = u,
                    userDetail = u.UserDetail,
                    School = new 
                    { 
                        Name = u.School!.Name,
                    }

                })
                .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(users,new JsonSerializerOptions() {WriteIndented = true }));
}

async Task GetAllUserDetails()
{
    var userDetails = await context.UserDetails.AsNoTracking()
        .Include(u => u.User)
        .Select(u => new
        {
            user =u.User,
            userDetail = u,
        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(userDetails, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task GetALLSchools()
{
    var schools = await context.Schools.AsNoTracking()
        .Include(s => s.Users)
        .ToListAsync();
    Console.WriteLine(JsonSerializer.Serialize(schools, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task GetAllTeachers()
{
    var teachers = await context.Teachers.AsNoTracking()
        .Include(t => t.TeacherUser)
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(teachers, new JsonSerializerOptions() { WriteIndented = true }));

}


//await AddData();
//await MultipleAddTeam();
//await ALlTeams();
//await GetAllLeagues();
//await FilterTeam(); 
//LinqSelect();
//await AddLeagues();
//await LeagueUpdate();

//Console.WriteLine("Enter league id");
//var leagueId = int.Parse(Console.ReadLine()!);
//await DeleteLeague(leagueId);

//await DeleteLeagues();

//var res = context.ChangeTracker.Entries();

//await GetAllPayers();

//await GetAllUsers();
//await GetAllUserDetails();
//await GetALLSchools();
await GetAllTeachers();

Console.ReadLine();
