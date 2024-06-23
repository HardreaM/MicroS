using Domain.Entities;

namespace Domain.Interfaces;

public interface IStorePost
{
    Task<Post[]> GetAllAsync();
    
    Task<Guid> AddPost(Post post);

    Task<Post> UpdatePost(Post post);

    Task<Post> ChangeUserNameById(Guid userId, string userName);
}