using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Helpers
{
    public class Validator
    {
        public static Dictionary<string, string> CreateTask(TaskVM task)
        {
            var response = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(task.Taskname))
            {
                response.Add("taskname", "Task name cannot be null");
            }

            if (string.IsNullOrEmpty(task.Description))
            {
                response.Add("description", "Description cannot be null");
            }

            if (task.EffortId < 1)
            {
                response.Add("effort", "You need to select effort");
            }

            if (task.PriorityId < 1)
            {
                response.Add("priority", "You need to select priority");
            }

            if (task.TeamId == null)
            {
                response.Add("teamid", "Team id unknown");
            }

            return response;
        }

        public static Dictionary<string, string> Register(UserVM user)
        {
            var response = Validator.Login(user);

            if (string.IsNullOrEmpty(user.Email))
            {
                response.Add("email", "Email cannot be null");
            }

            return response;
        }

        public static Dictionary<string, string> Login(UserVM user)
        {
            var response = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(user.UserName))
            {
                response.Add("username", "User name cannot be null");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                response.Add("password", "Password cannot be null");
            }

            return response;
        }

        internal static Dictionary<string, string> CreateTeam(TeamVM team)
        {
            var response = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(team.TeamName))
            {
                response.Add("teamname", "Team name cannot be null");
            }

            return response;
        }
    }
}