using MongoDB.Bson;
using MongoDB.Driver;
using BankUtils;

class DataBase
{
    private static readonly IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017/");
    private static readonly IMongoCollection<Account> mongoCollection = mongoClient.GetDatabase("Trabalho-TS").GetCollection<Account>("accounts");

    public Account FindAccount(string accountOwnerSSN)
    {
        var filter = Builders<Account>.Filter.Eq("OwnerSSN", accountOwnerSSN);
        return mongoCollection.Find(filter).FirstOrDefault();
    }

    public void InsertAccount(Account account)
    {
        mongoCollection.InsertOne(account);
    }

    public void UpdateAccount(Account account)
    {
        mongoCollection.ReplaceOne(new BsonDocument("OwnerSSN", account.OwnerSSN), account);
    }

    public void DeleteAccount(Account account)
    {
        var filter = Builders<Account>.Filter.Eq("OwnerSSN", account.OwnerSSN);
        mongoCollection.DeleteOne(filter);
    }
}

