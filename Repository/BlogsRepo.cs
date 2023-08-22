using System.Data;
using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using castlers.Common.AzureStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace castlers.Repository
{
    public class BlogsRepo : IBlogsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUploadFile _uploadFile;
        public BlogsRepo(ApplicationDbContext dbContext, IUploadFile uploadFile)
        {
            _dbContext = dbContext;
            _uploadFile = uploadFile;
        }
        public async Task<string?> AddBlogsAsync(Blogs blogs)
        {
            SaveDocResponseDto response = new SaveDocResponseDto();
            try
            {
                var path = string.Format("{0}/{1}/{2}", "Blogs", "Admin", blogs.file?.FileName);           
                if (blogs.file is not null)
                {
                    response = await UploadFile(path, blogs.file);
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@typeId", blogs.typeId));
                parameters.Add(new SqlParameter("@heading", blogs.heading));
                parameters.Add(new SqlParameter("@description", blogs.description));
                parameters.Add(new SqlParameter("@path", response.DocURL.IsNullOrEmpty() ? null : response.DocURL));
                parameters.Add(new SqlParameter("@isDeleted", Convert.ToBoolean(0)));
                parameters.Add(new SqlParameter("@createdBy", blogs.createdBy is not null ? 0 : 0));
                parameters.Add(new SqlParameter("@blogId", blogs.blogId is null ? 0 : blogs.blogId));

                parameters[6].Direction = ParameterDirection.Output;

                var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC AddNewBlog @typeId, @heading, @description, @path, @isDeleted, @createdBy, @blogId OUT", parameters));

                return parameters[6].Value is DBNull ? "0" : parameters[6].Value.ToString();            
            }
            catch (Exception) { throw; }
        }
        public async Task<List<Blogs>> GetAllBlogsAsync()
        {
            try
            {
                var blogsList = await Task.Run(() => _dbContext.Blogs
                    .FromSqlRaw(@"EXEC GetAllBlogs")
                    .AsEnumerable()
                    .Select(b => new Blogs
                    {
                        blogId = b.blogId,
                        typeId = b.typeId,
                        typeName = b.typeName,
                        heading = b.heading,
                        description = b.description
                    })
                    .ToList());

                return blogsList;
            }
            catch (Exception) { throw; }
        }
        public async Task<Blogs?> GetBlogByIdAsync(int blogId)
        {
            Blogs? blogDetails = new Blogs();
            try
            {
                SqlParameter para = new SqlParameter("@blogId", blogId);
                blogDetails = await Task.Run(() => _dbContext.Blogs.FromSqlRaw(@"EXEC GetBlogById @blogId", para)
                    .AsEnumerable().FirstOrDefault());

                return blogDetails;
            }
            catch (Exception) { throw; }
        }
        protected async Task<SaveDocResponseDto> UploadFile(string path, IFormFile file)
        {
            try
            {
                return await _uploadFile.SaveDoc(file, path);
            }
            catch (Exception) { throw; }
        }
    }
}
