using System;
using CLI.UI.ManagePosts;

using Entities;

using InMemoryRepositories;

using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class ManagePostsView
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly SinglePostView _singlePostView;
        private readonly IPostRepository postRepository;
        public ManagePostsView(IPostRepository postRepository)
        {
            _createPostView = new CreatePostVıew(this.postRepository);
            _listPostsView = new listPostsView(this.postRepository);
            _singlePostView = new singlePostView(this.postRepository);
            this.postRepository = postRepository;
        }

        public async Task ShowMenuAsync()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("Manage Posts Menu:");
                Console.WriteLine("1. Create Post");
                Console.WriteLine("2. List Posts");
                Console.WriteLine("3. View Single Post");
                Console.WriteLine("4. Update Post");
                Console.WriteLine("5. Delete Post");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await _createPostView.ShowCreatePostViewAsync();
                        break;
                    case "2":
                        await _listPostsView.ShowListPostViewAsync();
                        break;
                    case "3":
                        await _singlePostView.ViewSinglePostAsync();
                        break;
                    case "4":
                        await UpdatePostAsync();
                        break;
                    case "5":
                        await DeletePostAsync();
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task UpdatePostAsync()
        {
            Console.Write("Enter post ID to update: ");
            int postId = Convert.ToInt32(Console.ReadLine());

            Post post = await postRepository.GetSingleAsync(postId);
            if (post != null)
            {
                Console.Write("Enter new title: ");
                post.Title = Console.ReadLine();

                Console.Write("Enter new body: ");
                post.Body = Console.ReadLine();

                await postRepository.UpdateAsync(post);
                Console.WriteLine("Post updated successfully.");
            }
        }

        private async Task DeletePostAsync()
        {
            Console.Write("Enter post ID to delete: ");
            int postId =  Convert.ToInt32(Console.ReadLine());

            Post post = await postRepository.GetSingleAsync(postId);
            if (post != null)
            {
                await postRepository.DeleteAsync(postId);
                Console.WriteLine("Post deleted successfully.");
            }
        }
    }
}
