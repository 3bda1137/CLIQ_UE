using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Services
{
    public class BookMarkService : IBookMarkService
    {
        private readonly IMapper mapper;
        private readonly IBookMrakRepository bookMrakRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public BookMarkService(IBookMrakRepository bookMrakRepository , IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.bookMrakRepository = bookMrakRepository;
        }

        public async Task<bool> AddBookMark(BookMarkViewModel BookMarkVM)
        {
            BookMark bookMark = mapper.Map<BookMark>(BookMarkVM);

            await bookMrakRepository.AddBookMark(bookMark);
            return true;
        }
        public List<BookMark> GetAllBookMark(String UserID)
        {
            return bookMrakRepository.GetAllBookMarkList(UserID);
        }
    }
}
