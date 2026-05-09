using Azure.Core.Serialization;
using EFCore.data.Data;
using EFCore.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


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
        .ThenInclude(tu=> tu.User)
        .Select(x=> new
        {
            TeacherName = x.name,
            UserNames = x.TeacherUser.Select(u => u.User!.Name) 

        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(teachers, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task GetAllTU()
{
    var tu = (from i in context.TeacherUsers
              where i.User!.Name == "admin"
              select new { user = i.User, teacherName = i.Teacher!.name }).ToList();

    Console.WriteLine(JsonSerializer.Serialize(tu,new JsonSerializerOptions() { WriteIndented = true}));
}


//Explicity laading

async Task UserToUserDetailExplicity()
{
    var users = await context.Users.ToListAsync();

    foreach (var user in users)
    {
        context.Entry(user).Reference(u => u.UserDetail).Load();
        Console.WriteLine($"user name is {user.Name} and address is {user.UserDetail?.Address} \r\n" );
    }
}

async Task GetUsersFromSchool()
{
    var school =  context.Schools.FirstOrDefault(s => s.SchoolId == 1);

    await context.Entry(school!).Collection(s=>s.Users!).LoadAsync();

    //foreach (var user in school!.Users!)
    //{
    //    Console.WriteLine(JsonSerializer.Serialize(user,new JsonSerializerOptions() { WriteIndented = true }));
    //}

    var users = school!.Users!.ToList();
    Console.WriteLine(JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task GetTeacherFromUser()
{
    //var teachers = await context.Users.AsNoTracking()
    //     .Include(u => u.TeacherUser)
    //     .ThenInclude(u => u.Teacher)
    //     .Select(x=> new
    //     {
    //         users= x,
    //         teaxhers = x.TeacherUser.Select(x=>x.Teacher!.name)
    //     })
    //     .ToListAsync();

    var users = await context.Users.ToListAsync();


    foreach (var user in users)
    {
         context.Entry(user).Collection(x => x.TeacherUser).Load();
        var tu = user.TeacherUser.ToList();

        foreach (var teacher in tu)
        {
            context.Entry(teacher).Reference(x => x.Teacher).Load();

            Console.WriteLine(JsonSerializer.Serialize(teacher.Teacher!.name,new JsonSerializerOptions() { WriteIndented = true}));
        }

    }

    //context.Entry(tu).Reference(x=>x)

}


//Transaction

async Task TransactionTest()
{
    var transaction = context.Database.BeginTransaction();

    try
    {
        var newUser = new User { Email = "bgbg@123", Name = "bgbg" ,SchoolId = 1 };
        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
        var userId = newUser.UserId;

        var newUserDetail = new UserDetail { UserId = 3434 ,Address = "test",Age =22 };
        await context.UserDetails.AddAsync(newUserDetail);
        await context.SaveChangesAsync();
        transaction.Commit();
    }
    catch (Exception)
    {
        transaction.Rollback();
        throw;
    }

    //transactionStart.
}

async Task GetAllPosts()
{
    var posts = await context.Posts.AsNoTracking()
        //.Include(p => p.User)
        //.Include(p => p.CategoryPost)
        //.ThenInclude(p => p.Category)
        .Select(x => new
        {
            posts = x,
            user = x.User,
            Categories = x.CategoryPost.Select(x => x.Category).ToList(),
        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(posts, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task AllManagerFromTeam()
{
    var teams = await context.Teams.AsNoTracking()
        .Include(t => t.Manager)
        .Select(x => new
        {
            team = x,
            manager = x.Manager 
        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(teams, new JsonSerializerOptions() { WriteIndented = true }));

}

async Task AddMatches()
{
    var matches = new List<Match>()
    {
        new Match{HomeTeamId = 1, AwayTeamId = 2},
        new Match{HomeTeamId = 1, AwayTeamId = 3},
        new Match{HomeTeamId = 1, AwayTeamId = 4},
        new Match{HomeTeamId = 1, AwayTeamId = 5},
        new Match{HomeTeamId = 1, AwayTeamId = 6},

    };

    await context.Matches.AddRangeAsync(matches);
    var res = await context.SaveChangesAsync();

    Console.WriteLine("Added Matches");

}

async Task TeamTest()
{
    var result = await context.Teams.AsNoTracking()
        .Include(t=>t.Manager)
        .Include(t=>t.League)
        .Include(t=>t.Palyers)
        .Include(t => t.HomeMatches)
        .Include(t => t.AwayMatches)
        .ToListAsync();
     
    //var result = await context.Teams.FirstOrDefaultAsync(t => t.TeamId == 1);

    //context.Entry(result!).Collection(x => x!.HomeMatches).Load();
    //context.Entry(result!).Collection(x => x!.AwayMatches).Load();
    //context.Entry(result!).Reference(x => x.League).Load();
    //context.Entry(result!).Reference(x => x.Manager).Load();
    //context.Entry(result!).Collection(x=>x.Palyers!).Load();

    Console.WriteLine(JsonSerializer.Serialize(result,new JsonSerializerOptions() {WriteIndented = true }));
}

async Task LeaguesToTeams()
{
    var lt = await context.Leagues.AsNoTracking()
        .Include(l => l.Teams)
        .Select(x => new
        {
            x.Name,
            x.LeagueId,
            T  =x.Teams
        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(lt, new JsonSerializerOptions() { WriteIndented = true, }));
    //Console.WriteLine(JsonSerializer.Serialize(lt, new JsonSerializerOptions() { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve }));

}

async Task FromTeamToHomeTeam()
{
    var result = await context.Teams
        //.Include(q => q.HomeMatches)
        //.ThenInclude(x => x.HomeTeam)
        //.Include(q => q.AwayMatches)
        .Select(x => new
        {
            x.Name,
            x.League,
            x.Palyers,
            x.HomeMatches,
            x.Manager,
            x.AwayMatches,
            //hone_players = x.HomeMatches.Select(x=>x.HomeTeam.Palyers),
            //away_player = x.HomeMatches.Select(x=>x.AwayTeam.Palyers)

        })
        .ToListAsync();

    Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));

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
//await GetAllTeachers();
//await GetAllTU();

//await UserToUserDetailExplicity();
//await GetUsersFromSchool();

//await GetTeacherFromUser();

//await TransactionTest();

//await GetAllPosts();
//await AllManagerFromTeam();

//await AddMatches();

//await TeamTest();

//await LeaguesToTeams();

await FromTeamToHomeTeam();
Console.ReadLine();
