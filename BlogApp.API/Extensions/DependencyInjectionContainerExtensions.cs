using AutoMapper;
using BlogApp.API.Core;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.Logging;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Logging;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations;
using BlogApp.Implementations.Logging;
using BlogApp.Implementations.UseCases.Ef.Command;
using BlogApp.Implementations.UseCases.Ef.Query;
using BlogApp.Implementations.UseCases.Logging;
using BlogApp.Implementations.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.API.Extensions
{
    public static class DependencyInjectionContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<BlogAppDbContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddActionUser(this IServiceCollection services)
        {
            services.AddTransient<IActionUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claims = accessor.HttpContext.User;

                if(claims == null || claims.FindFirst("UserId") == null)
                {
                    return new ActionUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    AllowedUseCasesIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<RoleValidator>();
            services.AddTransient<CategoryValidator>();
            services.AddTransient<UserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<PostValidator>();
            services.AddTransient<CommentValidator>();
            services.AddTransient<UpdateCommentValidator>();
            services.AddTransient<SearchCommentLikesValidator>();
            services.AddTransient<CreateLikedCommentValidator>();
            services.AddTransient<SearchPostImageValidator>();
            services.AddTransient<CreatePostImageValidator>();
            services.AddTransient<DeletePostsImagesValidator>();
            services.AddTransient<CreatePostsLikeValidator>();
            services.AddTransient<DeletePostsLikeValidator>();
            services.AddTransient<UpdateUseCasesValidator>();
            services.AddTransient<UpdatePostValidator>();
            services.AddTransient<UpdatePostImagesValidator>();
            services.AddTransient<CreateNavigationLinkValidator>();
            services.AddTransient<UpdateNavigationLinkValidator>();
        }
        public static void AddUseCases(this IServiceCollection services)
        {

            services.AddRolesUseCases();

            services.AddCategoriesUseCases();

            services.AddUsersUseCases();

            services.AddPostsUseCases();

            services.AddUseCaseHandler();

            services.AddExceptionLogger();

            services.AddUseCaseLogger();

            services.AddCommentsUseCases();

            services.AddLikesCommentsUseCases();

            services.AddPostImagesUseCases();

            services.AddLikedPostsUseCases();

            services.AddUserUseCasesUseCases();
            services.AddNavigationLinksUseCases();

        }

        public static void CreateMaps(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<Comment, CommentDto>();
                cfg.CreateMap<User, UserDto>();
                //cfg.CreateMap<Post, SearchResultPostDto>();
            });
            services.AddTransient<IMapper>(x =>
            {
                return new Mapper(configuration);
            });
            //MyBootstraper.InitAutoMapper(cfg);
        }
        public static void AddRolesUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IGetRolesQuery, EfGetRolesQuery>();
            services.AddTransient<IFindRoleQuery, EfFindRoleQuery>();
            services.AddTransient<IUpdateRoleCommand, EfUpdateRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
        }
        public static void AddCategoriesUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IFindCategoryQuery, EfFindCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
        }
        public static void AddPostsUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<IFindPostQuery, EfFindPostQuery>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IUpdatePostCommand, EfUpdatePostCommand>();
        }
        public static void AddUsersUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
        }
        public static void AddUseCaseHandler(this IServiceCollection services)
        {
            services.AddTransient<UseCaseHandler>();
        }
        public static void AddExceptionLogger(this IServiceCollection services)
        {
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
        }
        public static void AddUseCaseLogger(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
        }
        public static void AddCommentsUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IFindCommentQuery, EfFindCommentQuery>();
            services.AddTransient<IUpdateCommentCommand, EfUpdateCommentCommand>();

        }
        public static void AddLikesCommentsUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCommentsLikesQuery, EfGetCommentsLikesQuery>();
            services.AddTransient<ICreateLikedCommentCommand, EfCreateLikedCommentCommand>();
            services.AddTransient<IDeleteLikedCommentCommand, EfDeleteLikedCommentCommand>();
        }
        public static void AddPostImagesUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetPostImagesQuery, EfGetPostImagesQuery>();
            services.AddTransient<ICreatePostImageCommand, EfCreatePostImageCommand>();
            services.AddTransient<IDeletePostsImagesCommand, EfDeletePostsImagesCommand>();
            services.AddTransient<IUpdatePostImagesCommand, EfUpdatePostImagesCommand>();
        }

        public static void AddLikedPostsUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetPostsLikesQuery, EfGetPostsLikesQuery>();
            services.AddTransient<ICreatePostsLikeCommand, EfCreatePostsLikeCommand>();
            services.AddTransient<IDeletePostsLikeCommand, EfDeletePostsLikeCommand>();
        }
        public static void AddUserUseCasesUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUpdateUseCasesCommand, EfUpdateUseCasesCommand>();
        }

       public static  void AddNavigationLinksUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetNavigationLinksQuery, EfGetNavigationLinksQuery>();
            services.AddTransient<ICreateNavigationLinkCommand, EfCreateNavigationLinkCommand>();
            services.AddTransient<IFindNavigationLinkQuery, EfFindNavigationLinkQuery>();
            services.AddTransient<IDeleteNavigationLinkCommand, EfDeleteNavigationLinkCommand>();
            services.AddTransient<IUpdateNavigationLinkCommand, EfUpdateNavigationLinkCommand>();
        }
    }
}
