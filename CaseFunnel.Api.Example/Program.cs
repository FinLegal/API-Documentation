using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;


string baseUrl = "https://api.casefunnel.io";
string apiKey = "";
string caseId = "";


string emailAddressToFind = "adam+test1@disputed.io";

var client = await HttpGetJsonAsync<PagedList<Client>>($"clients/by-email/{emailAddressToFind}");


var activities = await HttpGetJsonAsync<List<Activity>>($"clients/{client.Items[0].Id}/activities");


var notes = await HttpGetJsonAsync<PagedList<ClientNote>>($"clients/{client.Items[0].Id}/notes");


//upload a file to a client
using var fileStream = System.IO.File.OpenRead("test.txt");

var fileMetadata = await $"{baseUrl}/funnel/v1/cases/{caseId}/clients/{client.Items[0].Id}/files"
    .WithHeader("X-Api-Key", apiKey)
    .WithHeader("X-file-name", "test.txt")
    .WithHeader("X-file-type", "application/txt")
    .WithHeader("Content-Length", fileStream.Length)
    .PostAsync(new StreamContent(fileStream));


async Task<T> HttpGetJsonAsync<T>(string path)
{
    return await $"{baseUrl}/funnel/v1/cases/{caseId}/{path}"
        .WithHeader("X-Api-Key", apiKey)
        .GetJsonAsync<T>();
}

public class PagedList<T>
{
    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int ItemsCount { get; set; }
}

public record ClientNote(Guid Id, DateTime CreationDateTime, Guid ClientId, string Type, string Text);
public class Client
{
    public Guid Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }
    
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    
    public Address Address { get; set; }

    public List<ClientAttribute> Attributes { get; set; }
    
    public List<string> MarketingConsents { get; set; }
    
    public List<string> ServiceConsents { get; set; }
}

public record Address(string AddressLine1, string AddressLine2, string City, string State, string Country,
    string PostCode);

public abstract class Attribute
{
    public string StringValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public bool? BoolValue { get; set; }
    public double? DoubleValue { get; set; }
    public int? IntValue { get; set; }
}

public class ClientAttribute : Attribute
{
    public Guid Id { get; set; }
    public Guid ClientAttributeTemplateId { get; set; }
}


public class ActivityAttribute : Attribute
{
    public Guid Id { get; set; }
    public Guid ActivityAttributeTemplateId { get; set; }
}

public class Activity
{
    public Guid Id { get; set; }
    public Guid ActivityTemplateId { get; set; }
    public string Status { get; set; }
    public List<ActivityAttribute> Attributes { get; set; } = new();
}

public record File(Guid Id, DateTime CreationDateTime, string Name, string ShareType, string ContentType, long Length, Guid? ActivityId);