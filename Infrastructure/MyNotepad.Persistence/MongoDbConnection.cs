namespace MyNotepad.Persistence;
using MongoDB.Driver;

public class MongoDbConnection
{
    private static readonly object lockObject = new object();
    private static MongoClient client;
    private static readonly string connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING")!; // Replace this with your actual connection string
    private static readonly string databaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME")!; // Replace this with your actual database name

    private static IMongoDatabase database;

    private MongoDbConnection() { }

    public static IMongoDatabase GetDatabaseConnection()
    {
        if (database == null)
        {
            lock (lockObject) /// Prevent simultaneously value assignment
            {
                if (database == null)
                {
                    client = new MongoClient(connectionString);
                    database = client.GetDatabase(databaseName);
                }
            }
        }
        return database;
    }
}
