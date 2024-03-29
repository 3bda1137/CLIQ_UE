using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class EditRepository : IEditRepository
    {
        private readonly ApplicationContext context;

        public EditRepository(ApplicationContext _context)
        {
            context = _context;
        }


        //public void Save(EditBioAndUploadImageViewModel editBioVm)
        //{
        //    context.SaveChanges();
        //}
        public void insert(EditBio obj)
        {
            context.Add(obj);
        }

        public void Save(EditBio obj)
        {
            context.SaveChanges();
        }
    }
}
