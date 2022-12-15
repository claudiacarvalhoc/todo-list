using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Core.Entities;

public class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public bool Deleted { get; set; }

    public override string ToString()
    {
        return $"[Id: {this.Id}, Name: {this.Name}, Description: {this.Description}, Status: {this.Status.ToString()}, Deleted: {this.Deleted}]";
    }
}