using CLI.UI;

using FileRepositories;

using RepositoryContracts;

Console.WriteLine("Starting CLI App..");
IUserRepository userRepository = new UserFileRepository();
IPostRepository postRepository = new PostFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();

CliApp cliApp = new(userRepository, postRepository, commentRepository);
await cliApp.StartAsync();
