using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Domain.Tests;

public class CourseListingTests
{
    [Fact]
    public void Constructor_CreatesListing()
    {
        var id = Guid.NewGuid();
        var listing = new CourseListing(id, "C# Basics", "Learn C#");

        Assert.Equal(id, listing.Id);
        Assert.Equal("C# Basics", listing.Title);
        Assert.False(listing.IsFavourite);
    }

    [Fact]
    public void AddToFavourites_SetsFavourite()
    {
        var listing = new CourseListing(Guid.NewGuid(), "C#", "Description");

        listing.AddToFavourites();

        Assert.True(listing.IsFavourite);
    }

    [Fact]
    public void RemoveFromFavourites_ClearsFavourite()
    {
        var listing = new CourseListing(Guid.NewGuid(), "C#", "Description");
        listing.AddToFavourites();

        listing.RemoveFromFavourites();

        Assert.False(listing.IsFavourite);
    }
}
