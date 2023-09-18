using LibGit2Sharp;
using System.Diagnostics;
using LibGit2Sharp.Handlers;

namespace Workflower.Logic.Entities.Internal;

internal class RepositoryActions : IRepositoryActions
{
    public RepositoryActions(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public void PullBranchAndCheckoutNew(string branchToPull, string branchToCheckout)
    {
        Repo(repo =>
        {
            var branch = repo.Branches.First(x => x.FriendlyName == branchToPull);
            Commands.Checkout(repo, branch);

            Commands.Pull(repo, repo.Config.BuildSignature(DateTimeOffset.Now), new PullOptions
            {
                FetchOptions = new FetchOptions
                {
                    CredentialsProvider = CredentialsProvider(),
                },
                MergeOptions = new MergeOptions
                {
                    FailOnConflict = true
                }
            });

            var newBranch = repo.CreateBranch(branchToCheckout);

            Commands.Checkout(repo, newBranch);
        });
    }

    public void StageAllAndCommit(string message)
    {
        Repo(repo =>
        {
            var signature = repo.Config.BuildSignature(DateTimeOffset.Now);

            Commands.Stage(repo, "*");
            repo.Commit(message, signature, signature);
        });
    }

    public void Push()
    {
        Repo(repo =>
        {
            var pushOptions = new PushOptions
            {
                CredentialsProvider = CredentialsProvider()
            };

            if (!repo.Head.IsTracking)
            {
                var remote = repo.Network.Remotes["origin"];
                repo.Branches.Update(repo.Head,
                    b => b.Remote = remote.Name,
                    b => b.UpstreamBranch = repo.Head.CanonicalName);
            }

            repo.Network.Push(repo.Head, pushOptions);
        });
    }

    private static CredentialsHandler CredentialsProvider()
    {
        return (url, usernameFromUrl, types) =>
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "git.exe",
                Arguments = "credential fill",
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            // Write query to stdin. 
            // For stdin to work we need to send \n instead of WriteLine
            // We need to send empty line at the end
            var uri = new Uri(url);
            process.StandardInput.NewLine = "\n";
            process.StandardInput.WriteLine($"protocol={uri.Scheme}");
            process.StandardInput.WriteLine($"host={uri.Host}");
            process.StandardInput.WriteLine($"path={uri.AbsolutePath}");
            process.StandardInput.WriteLine();

            // Get user/pass from stdout
            string? username = null;
            string? password = null;
            string? line;
            while ((line = process.StandardOutput.ReadLine()) != null)
            {
                string[] details = line.Split('=');
                if (details[0] == "username")
                {
                    username = details[1];
                }
                else if (details[0] == "password")
                {
                    password = details[1];
                }
            }

            return new UsernamePasswordCredentials()
            {
                Username = username,
                Password = password
            };
        };
    }

    public void Pull()
    {
        Repo(repo =>
        {
            Commands.Pull(repo, repo.Config.BuildSignature(DateTimeOffset.Now), new PullOptions
            {
                FetchOptions = new FetchOptions
                {

                },
                MergeOptions = new MergeOptions
                {
                    FailOnConflict = true
                }
            });
        });
    }

    private void Repo(Action<LibGit2Sharp.Repository> act)
    {
        using (var repo = new LibGit2Sharp.Repository(Path))
        {
            act(repo);
        }
    }
}