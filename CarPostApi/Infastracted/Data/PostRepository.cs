using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infastracted.Data;

internal class PostRepository : IStorePost
{
    private readonly CarPostInfoContext postContext;

    public PostRepository(CarPostInfoContext postContext)
    {
        this.postContext = postContext;
    }

    public async Task<Post[]> GetAllAsync()
    {
        return await postContext.Posts.ToArrayAsync();
    }

    public async Task<Guid> AddPost(Post post)
    {
        await postContext.Posts.AddAsync(post);
        await postContext.SaveChangesAsync();

        return post.Id;
    }

    public async Task<Post> UpdatePost(Post post)
    {
        var result = await postContext.Posts.FindAsync(post.Id);
        if (result != null)
        {
            postContext.Entry(result).CurrentValues.SetValues(post);
        }

        await postContext.SaveChangesAsync();

        return post;
    }

    public async Task<Post> ChangeUserNameById(Guid userId, string userName)
    {
        var post = await postContext.Posts.SingleAsync(p => p.UserId == userId);
        var newPost = post with { UserInfo = post.UserInfo with { Name = userName} };
        postContext.Entry(post).CurrentValues.SetValues(newPost);
        await postContext.SaveChangesAsync();

        return newPost;
    }
}