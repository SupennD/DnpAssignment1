using CLI.UI;

using InMemoryRepositories;

using RepositoryContracts;

Console.WriteLine("Starting CLI App..");
IUserRepository userRepository = new UserInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();

CliApp cliApp = new(userRepository, postRepository, commentRepository);
await cliApp.StartAsync();
