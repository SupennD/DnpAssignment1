using Entities;

using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class ManagePostsView
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly SinglePostView _singlePostView;
        private readonly IPostRepository _postRepository;
        public ManagePostsView(IPostRepository postRepository)
        {
            _createPostView = new CreatePostView(postRepository);
            _listPostsView = new ListPostsView(postRepository);
            _singlePostView = new SinglePostView(postRepository);
            _postRepository = postRepository;
        }

        public async Task ShowMenuAsync()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\nManage Posts Menu:");
                Console.WriteLine("1. Create Post");
                Console.WriteLine("2. List Posts");
                Console.WriteLine("3. View Single Post");
                Console.WriteLine("4. Update Post");
                Console.WriteLine("5. Delete Post");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

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
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        private async Task UpdatePostAsync()
        {
            try
            {
                Console.Write("\nEnter post ID to update: ");

                int postId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The post ID is required."));
                Post post = await _postRepository.GetSingleAsync(postId);

                Console.Write("Enter new title: ");

                post.Title = Console.ReadLine() ?? throw new ArgumentException("The title is required.");

                Console.Write("Enter new body: ");

                post.Body = Console.ReadLine() ?? throw new ArgumentException("The body is required.");
                await _postRepository.UpdateAsync(post);

                Console.WriteLine("Post updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task DeletePostAsync()
        {
            try
            {
                Console.Write("\nEnter post ID to delete: ");

                int postId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The post ID is required."));
                await _postRepository.DeleteAsync(postId);

                Console.WriteLine("Post deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
