using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public void Send_PrivateMessage(string msgFrom, string msg, string touserid)
        {
            var id = Context.ConnectionId;
            // šaljemo korisniku koji šalje da ima svoju kopiju
            Clients.Client(id).SendAsync("broadcastMessage", msgFrom, msg);
            // šaljemo korisniku kojem šalje da i on vidi
            Clients.Client(touserid).SendAsync("broadcastMessage", msgFrom, msg);

            //cli.Caller.receiveMessage(msgFrom, msg, touserid);
            //cli.Client(touserid).receiveMessage(msgFrom, msg, id);
        }

        [HubMethodName("hubconnect")]
        public void Get_Connect(string username, string userId, string connectionId)
        {
            string count = "1";
            string msg = "testgetconnect";
            string list = "dsada";

            //try
            //{
            //    count = GetCount().ToString();
            //    msg = updaterec(username, userId, connectionId);
            //    list = GetUsers(username);
            //}
            //catch(Exception e)
            //{
            //    msg = "DB Error " + e.Message;
            //}

            var id = Context.ConnectionId;

            string[] Exceptional = new string[1];
            Exceptional[0] = id;
            Clients.All.SendAsync("receiveMessage", "ChatHub", msg, list);
            //cli.Caller.receiveMessage("RU", msg, list);
            //cli.AllExcept(Exceptional).receiveMessage("NewConnection", username + " " + id, count);
            Clients.AllExcept(Exceptional).SendAsync("receiveMessage", "ChatHub", username + " " + id);
        }
        
        public override Task OnConnectedAsync()
        {
            //string username = Context.QueryString["username"].ToString();
            string clientId = Context.ConnectionId;
            string data = clientId;
            string count = "1";
            //try
            //{
            //    count = GetCount().ToString();
            //}
            //catch (Exception d)
            //{
            //    count = d.Message;
            //}
            //var a = cli.Client(clientId);
            //var b = cli.Caller;
            Clients.All.SendAsync("receiveMessage", "ChatHub", data, count);
            //cli.Caller.receiveMessage("ChatHub", data, count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            //string count = "";
            //string msg = "";

            //string clientId = Context.ConnectionId;
            //DeleteRecord(clientId);

            //try
            //{
            //    count = GetCount().ToString();
            //}
            //catch (Exception d)
            //{
            //    msg = "DB Error " + d.Message;
            //}
            //string[] Exceptional = new string[1];
            //Exceptional[0] = clientId;
            //cli.AllExcept(Exceptional).receiveMessage("NewConnection", clientId + " leave", count);

            return base.OnDisconnectedAsync(e);
        }

        //public string updaterec(string username, string userid, string connectionid)
        //{
        //    try
        //    {
        //        SqlCommand save = new SqlCommand("insert into [ChatUsers] values('" + username + "','" + userid + "','" + connectionid + "')", sqlcon);
        //        sqlcon.Open();
        //        int rs = save.ExecuteNonQuery();
        //        sqlcon.Close();
        //        return "saved";
        //    }
        //    catch (Exception d)
        //    {
        //        sqlcon.Close();
        //        return d.Message;
        //    }
        //}

        //public int GetCount()
        //{
        //    int count = 0;

        //    try
        //    {
        //        SqlCommand getCount = new SqlCommand("select COUNT([UserName]) as TotalCount from [ChatUsers]", sqlcon);
        //        sqlcon.Open();
        //        count = int.Parse(getCount.ExecuteScalar().ToString());
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    sqlcon.Close();
        //    return count;
        //}

        //public bool DeleteRecord(string connectionid)
        //{
        //    bool result = false;

        //    try
        //    {
        //        SqlCommand deleterec = new SqlCommand("delete from [ChatUsers] where ([ConnectionID]='" + connectionid + "')", sqlcon);
        //        sqlcon.Open();
        //        deleterec.ExecuteNonQuery();
        //        result = true;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    sqlcon.Close();
        //    return result;
        //}

        //public string GetUsers(string username)
        //{
        //    string list = "";

        //    try
        //    {
        //        int count = GetCount();
        //        SqlCommand listrec = new SqlCommand("select [UserName],[ConnectionID] from [ChatUSers] where ([UserName]<>'" + username + "')", sqlcon);
        //        sqlcon.Open();
        //        SqlDataReader reader = listrec.ExecuteReader();
        //        reader.Read();

        //        for (int i = 0; i < (count - 1); i++)
        //        {
        //            list += reader.GetValue(0).ToString() + " ( " + reader.GetValue(1).ToString() + " )#";
        //            reader.Read();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    sqlcon.Close();
        //    return list;
        //}

        //public void Create_Group(string GroupName)
        //{

        //}

        //private string GetClientId()
        //{
        //    string clientId = "";
        //    if (Context.QueryString["clientId"] != null)
        //    {
        //        // clientId passed from application 
        //        clientId = this.Context.QueryString["clientId"];
        //    }

        //    if (string.IsNullOrEmpty(clientId.Trim()))
        //    {
        //        clientId = Context.ConnectionId;
        //    }

        //    return clientId;
        //}
    }
}