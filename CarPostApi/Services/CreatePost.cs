using Domain.Entities;
using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Intefaces;

namespace Services;

internal class CreatePost : ICreatePost
{
    private readonly IStorePost _storePost;
    private readonly ICheckUser _checkUser;
    private readonly IProfileConnectionServcie _profileConnectionServcie;

    public CreatePost(IStorePost storePost, ICheckUser checkUser, IProfileConnectionServcie profileConnectionServcie)
    {
        _storePost = storePost;
        _checkUser = checkUser;
        _profileConnectionServcie = profileConnectionServcie;
    }

    public async Task<Guid> CreatePostAsync(Post post)
    {
        await _checkUser.CheckUserExistAsync(post.UserId);
        var newPost = post with { Id = new Guid() };
        await _storePost.AddPost(newPost);
        return newPost.Id;
    }

    public async Task<Post[]> GetPostListAsync()
    {
        var posts = await _storePost.GetAllAsync();
        var guids = posts.Select(p => p.UserId).ToArray();

        var users = await _profileConnectionServcie.GetUserNameListAsync(new UserNameListProfileApiRequest
        {
            guids = guids
        });

        var ans = posts.Select(p =>
        {
            var x = users.First(u => u.Id == p.UserId);
            return p with { UserInfo = new CreatedPostUserInfo { Name = x.Name, Surname = x.Surname } };
        }).ToArray();

        return ans;
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        return await _storePost.UpdatePost(post);
    }
}