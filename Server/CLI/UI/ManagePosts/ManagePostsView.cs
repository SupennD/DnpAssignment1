using System;
using CLI.UI.ManagePosts;

namespace CLI.UI.ManagePosts
{
    public class ManagePostsView
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly SinglePostView _singlePostView;

        public ManagePostsView(CreatePostView createPostView, ListPostsView listPostsView, SinglePostView singlePostView)
        {
            _createPostView = createPostView;
            _listPostsView = listPostsView;
            _singlePostView = singlePostView;
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
                        _createPostView.CreatePost();
                        break;
                    case "2":
                        _listPostsView.ListPosts();
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
            int postId = int.Parse(Console.ReadLine());

            var post = _singlePostView.GetPostById(postId);
            if (post != null)
            {
                Console.Write("Enter new title: ");
                post.Title = Console.ReadLine();

                Console.Write("Enter new body: ");
                post.Body = Console.ReadLine();

                _singlePostView.UpdatePost(post);
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
            int postId = int.Parse(Console.ReadLine());

            var post = _singlePostView.GetPostById(postId);
            if (post != null)
            {
                _singlePostView.DeletePost(postId);
                Console.WriteLine("Post deleted successfully.");
            }
            else
            {
                Console.WriteLine("Post not found.");
            }
        }
    }
}
