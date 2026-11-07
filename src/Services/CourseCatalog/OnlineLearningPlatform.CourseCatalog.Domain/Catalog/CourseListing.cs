namespace OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

public sealed class CourseListing
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public bool IsFavourite { get; private set; }

    public CourseListing(Guid id, string title, string description)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Course ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Course title cannot be empty.");

        Id = id;
        Title = title;
        Description = description;
    }

    public void AddToFavourites()
    {
        IsFavourite = true;
    }

    public void RemoveFromFavourites()
    {
        IsFavourite = false;
    }
}