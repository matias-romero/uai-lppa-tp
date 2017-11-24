using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaludArTE.Models.Repositories
{
    public interface IUsersRepository
    {
        User Get(Guid id);
        User Get(string username);
        User ValidateCredentials(string username, string password);
    }

    public class UsersRepository : IUsersRepository
    {
        private JArray LoadFromInternalFile()
        {
            var filename = HttpContext.Current.Server.MapPath("~/App_Data/Users.json");
            return JArray.Parse(File.ReadAllText(filename, Encoding.UTF8));
        }

        public User Get(Guid id)
        {
            var users = this.LoadFromInternalFile();
            var jObject = (JObject)users.FirstOrDefault(obj => obj.Value<Guid>("Id").Equals(id));
            return MapFromJson(jObject);
        }

        public User Get(string username)
        {
            var users = this.LoadFromInternalFile();
            var jObject = (JObject)users.FirstOrDefault(obj => obj.Value<string>("Username").Equals(username));
            return MapFromJson(jObject);
        }

        public User ValidateCredentials(string username, string password)
        {
            var users = this.LoadFromInternalFile();
            var jObject = (JObject)users.SingleOrDefault(obj => obj.Value<string>("Username").Equals(username) && obj.Value<string>("Password").Equals(password));
            return MapFromJson(jObject);
        }

        private User MapFromJson(JObject jsonUser)
        {
            return jsonUser == null ? null : JsonConvert.DeserializeObject<User>(jsonUser.ToString());
        }
    }
}