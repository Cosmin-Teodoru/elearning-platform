namespace Elearn.Shared.Models;

public class Question
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    
    public string Description { get; set; }
    public string Url { get; set; }
    public DateTime CreationDate { get; set; }

    public bool CorrectAnswer { get; set; }
    
    public Student Author { get; set; }

    public Question(long id, string title, string body, string description, string url, DateTime creationDate, bool correctAnswer, Student author)
    {
        Id = id;
        Title = title;
        Body = body;
        Description = description;
        Url = url;
        CreationDate = creationDate;
        CorrectAnswer = correctAnswer;
        Author = author;
    }

    public Question(string title, string body, string description, string url, DateTime creationDate, bool correctAnswer, Student author)
    {
        Title = title;
        Body = body;
        Description = description;
        Url = url;
        CreationDate = creationDate;
        CorrectAnswer = correctAnswer;
        Author = author;
    }

    public Question()
    {
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Body: {Body}, Url: {Url}, CreationDate: {CreationDate}, CorrectAnswerId: {CorrectAnswer}, Author: {Author}";
    }
}