using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DbUp;

namespace Art.Database
{
    internal class Program
    {
        private const int NoMigrationsRequired = 1;
        private const int ConnectionFailed = 2;
        private const int UpgraderFailed = 3;

        private const string JournalingSchema = "dbo";
        private const string JournalingTable = "Migrations";

        private static readonly string ScriptRoot =
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "Migrations");

        private const string localConnectionString = "Server=(local)\\LOCALDEV; Database=ART; Trusted_connection=true";

        private const string azureConnectionString = "azure-db";

        private static int Main()
        {
            var connectionString = localConnectionString;

            Console.WriteLine($"Using connection string '{ connectionString }'.");
            Console.WriteLine($"Using scripts root '{ ScriptRoot }'.");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var migrationEngine = DeployChanges.To.SqlDatabase(connectionString);

            foreach (var dir in Directory.EnumerateDirectories(ScriptRoot))
            {
                migrationEngine.WithScriptsFromFileSystem(dir, s => Regex.IsMatch(s, @"[\d]{3}.*\.sql"));
            }

            migrationEngine
                .LogScriptOutput()
                .LogToConsole()
                .JournalToSqlTable(JournalingSchema, JournalingTable);

            var upgrader = migrationEngine.Build();

            if (!upgrader.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("There are no any migrations to perform.");
                Console.ResetColor();

                foreach (var script in upgrader.GetExecutedScripts())
                {
                    Console.WriteLine($"Script was executed before: { script }");
                }

                Console.WriteLine("Exiting...");
                return NoMigrationsRequired;
            }

            if (!upgrader.TryConnect(out var connectionMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection could be established, message: ");
                Console.WriteLine(connectionMessage);
                Console.ResetColor();

                Console.WriteLine("Exiting...");
                return ConnectionFailed;
            }

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

                Console.WriteLine("Exiting...");
                return UpgraderFailed;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            Console.WriteLine("Exiting...");
            return 0;
        }
    }
}
