using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Core.Dal.Base;

namespace Domain.Entities;

public partial record Post : BaseEntityDal<Guid>
{
    /// <summary>
    ///
    /// </summary>
    public required Guid UserId { get; init; }
    
    [NotMapped]
    public CreatedPostUserInfo UserInfo { get; init; }

    public required Guid CarId { get; init; }
    
    [NotMapped]
    public CreatedPostCarInfo CarInfo { get; init; }

    public required string Title { get; init; }

    public required string Content { get; init; }

    public int LikeCount { get; init; }
}

public record CreatedPostUserInfo
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
}

public record CreatedPostCarInfo
{
    public required string Model { get; init; }
}

public partial record Post : BaseEntityDal<Guid>
{
    public async Task<Guid> SaveAsync(
        ICheckUser checkUser,
        IStorePost storePost)
    {
        // 1 транзакция

        // 2 транзакция 

        // throw exception
        await checkUser.CheckUserExistAsync(UserId);
        var id = await storePost.AddPost(this);
        //Id = id;
        return id;
    }
}