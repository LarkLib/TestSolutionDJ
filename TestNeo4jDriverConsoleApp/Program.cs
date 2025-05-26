using Neo4j.Driver;
namespace TestNeo4jDriverConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        var uri = "bolt://192.168.2.45:7687";
        var user = "neo4j";
        var password = "Elite123";
        var database = "tdb";

        var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        var session = driver.AsyncSession(o => o.WithDatabase(database));

        // Create nodes and relationships
        await session.ExecuteWriteAsync(async tx =>
        {
            // Create 10 nodes
            for (int i = 1; i <= 10; i++)
            {
                await tx.RunAsync($"CREATE (n:Nodes {{id: {i}, name: 'Node{i}', status: 'active'}})");
            }

            // Create 20 relationships
            for (int i = 1; i <= 20; i++)
            {
                int startNodeId = (i % 10) + 1;
                int endNodeId = ((i + 1) % 10) + 1;

                await tx.RunAsync($"MATCH (a:Nodes {{id: {startNodeId}}}), (b:Nodes {{id: {endNodeId}}}) CREATE(a) - [:Paths]->(b)");
            }
        });

        await session.ExecuteReadAsync(async tx =>
        {
            var cursor = await tx.RunAsync("MATCH (n:Nodes) RETURN n {.*} LIMIT 25");

            var records = await cursor.ToListAsync(record =>
                new Node
                {
                    Id     = (record["n"]  as Dictionary<string, object>)["id"].As<int>(),
                    Name   = (record?["n"] as Dictionary<string, object>)["name"].As<string>(),
                    Status = (record?["n"] as Dictionary<string, object>)["status"].As<string>()
                });

        });

        await session.CloseAsync();
        await driver.DisposeAsync();

        Console.WriteLine("Nodes and relationships created successfully in the 'tdb' database.");
    }
}

public class Node
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
}
