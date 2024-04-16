using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Identity;

public class BookMarkService : IBookMarkService
{
    private readonly IMapper mapper;
    private readonly IBookMrakRepository bookMrakRepository;
    private readonly UserManager<ApplicationUser> userManager;

    public BookMarkService(IBookMrakRepository bookMrakRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.bookMrakRepository = bookMrakRepository;
    }



    public void AddBookMark(BookMark BookMarkVM)
    {



        bookMrakRepository.AddBookMark(BookMarkVM);
    }

    public List<BookMark> GetAllBookMark(string UserID)
    {

        return bookMrakRepository.GetAllBookMarkList(UserID);

    }

    public List<int> getAllPostsId(string userId)
    {
        return bookMrakRepository.getAllPostsId(userId);
    }

    public bool isBookmarked(int postId, string userId)
    {
        return bookMrakRepository.isBookmarked(postId, userId);
    }

    public void removeBookmark(int postId, string userId)
    {
        bookMrakRepository.removeBookmark(postId, userId);
    }
}
