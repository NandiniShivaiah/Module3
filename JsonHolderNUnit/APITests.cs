using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonHolderNUnit
{
    [TestFixture]
    internal class APITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        List<UserData> data;

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }

        [Test]

        public void GetAllPosts()
        {
            var GetAllRequest = new RestRequest("posts", Method.Get);
            var GetAllResponse = client.Execute(GetAllRequest);

            Assert.That(GetAllResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
           Assert.IsNotEmpty(GetAllResponse.Content);
            Console.Write(GetAllResponse.Content);


        }
        [Test]
        public void GetOnePost()
        {
            var GetOneRequest = new RestRequest("posts/1", Method.Get);
            var GetOneResponse=client.Execute(GetOneRequest);

            Assert.That(GetOneResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var response=JsonConvert.DeserializeObject<UserData>(GetOneResponse.Content);

            Console.WriteLine($"Response:{response}");
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Body);
        }

        [Test]

        public void GetAllCommentsPost()
        {
            var GetAllCommentRequest = new RestRequest("posts/1/comments", Method.Get);
            var GetAllCommentResponse=client.Execute(GetAllCommentRequest);

            Assert.That(GetAllCommentResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

          
           

            Assert.IsNotEmpty(GetAllCommentResponse.Content);
            
        }

        [Test]
        public void GetCommentsForOnePost()
        {
            var GetOneCommentRequest = new RestRequest("comments", Method.Get);
            GetOneCommentRequest.AddQueryParameter("postId", "1");
            var getOneCommentResponse = client.Execute(GetOneCommentRequest);
            Assert.That(getOneCommentResponse.StatusCode == System.Net.HttpStatusCode.OK);

            List<CommentData> commentData = JsonConvert.DeserializeObject<List<CommentData>>(getOneCommentResponse.Content);

            Assert.IsNotNull(commentData);
            Assert.That(commentData[0].PostId, Is.EqualTo(1));

        }

        [Test]
        public void UpdatePost()
        {
            var updatePostRequest = new RestRequest("posts/1", Method.Put);
            updatePostRequest.AddHeader("Content-Type", "application/json");
            updatePostRequest.AddJsonBody(new
            {
                userId = 2,
                id = 12,
                title = "comics",
                body = "empty"
            });
            var updatePostResponse = client.Execute(updatePostRequest);
            Assert.That(updatePostResponse.StatusCode == System.Net.HttpStatusCode.OK);
            var response = JsonConvert.DeserializeObject<UserData>(updatePostResponse.Content);

            Assert.IsNotNull(response);
            Assert.That(response.UserId.Equals(2));

        }

        [Test]

        public void DeletePost()
        {
            var deletePostRequest = new RestRequest("posts/1", Method.Delete);
            var deletePostResponse = client.Execute(deletePostRequest);
            Assert.That(deletePostResponse.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.IsNotNull(deletePostResponse.Content);


        }
    }
}
