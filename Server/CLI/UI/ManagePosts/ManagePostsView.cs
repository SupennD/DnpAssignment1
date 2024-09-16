using System;
using CLI.UI.ManagePosts;

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
        public ManagePostsView(CreatePostView createPostView, ListPostsView listPostsView, SinglePostView singlePostView, IPostRepository postRepository)
        {
            _createPostView = createPostView;
            _listPostsView = listPostsView;
            _singlePostView = singlePostView;
            this.postRepository = postRepository;
        }

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Manage Posts Menu:");
                Console.WriteLine("1. Create Post");
                Console.WriteLine("2. List Posts");
                Console.WriteLine("3. View Single Post");
                Console.WriteLine("4. Update Post");
                Console.WriteLine("5. Delete Post");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _createPostView.ShowCreatePostView();
                        break;
                    case "2":
                        _listPostsView.ShowListPostView();
                        break;
                    case "3":
                        _singlePostView.ViewSinglePost();
                        break;
                    case "4":
                        UpdatePost();
                        break;
                    case "5":
                        DeletePost();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void UpdatePost()
        {
            Console.Write("Enter post ID to update: ");
            int postId = Convert.ToInt32(Console.ReadLine());

            var post = _singlePostView.GetPostById(postId);
            if (post != null)
            {
                Console.Write("Enter new title: ");
                post.Title = Console.ReadLine();

                Console.Write("Enter new body: ");
                post.Body = Console.ReadLine();

                postRepository.UpdateAsync(post);
                Console.WriteLine("Post updated successfully.");
            }
            else
            {
                Console.WriteLine("Post not found.");
            }
        }

        private void DeletePost()
        {
            Console.Write("Enter post ID to delete: ");
            int postId =  Convert.ToInt32(Console.ReadLine());

            var post = _singlePostView.GetPostById(postId);
            if (post != null)
            {
                postRepository.DeleteAsync(postId);
                Console.WriteLine("Post deleted successfully.");
            }
            else
            {
                Console.WriteLine("Post not found.");
            }
        }
    }
}
