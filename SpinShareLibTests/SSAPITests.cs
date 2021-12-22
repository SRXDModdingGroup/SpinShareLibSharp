using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpinShareLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace SpinShareLib.Tests
{
    [TestClass()]
    public class SSAPITests
    {
        public SSAPI inst;
        public SSAPITests()
        {
            inst = new SSAPI();
        }

        [TestMethod()]
        public void ping()
        {
            Task.Run(async () => {
               var thing = await inst.ping();
               Console.WriteLine(thing.status);
            }).GetAwaiter().GetResult();
            
        }
        [TestMethod()]
        public void getPromos()
        {
            Task.Run(async () => {
                var thing = await inst.getPromos();
                Console.WriteLine(thing.data[0].button.data);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getNewSongs()
        {
            Task.Run(async () => {
                var thing = await inst.getNewSongs(0);
                Console.WriteLine(thing.data[0].id);
            }).GetAwaiter().GetResult();

        }
        [TestMethod()]
        public void getHotThisWeekSongs()
        {
            Task.Run(async () => {
                var thing = await inst.getHotThisWeekSongs(0);
                Console.WriteLine(thing.data[0].id);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getSongDetail()
        {
            Task.Run(async () => {
                var thing = await inst.getSongDetail("1234");
                Console.WriteLine(thing.data.uploadDate.date);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getSongDetailReviews()
        {
            Task.Run(async () => {
                var thing = await inst.getSongDetailReviews("1234");
                Console.WriteLine(thing.data.reviews[0].comment);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getSongDetailSpinPlays()
        {
            Task.Run(async () => {
                var thing = await inst.getSongDetailSpinPlays("1234");
                Console.WriteLine(thing.data.spinPlays[0].videoUrl);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getUserDetail()
        {
            Task.Run(async () => {
                var thing = await inst.getUserDetail("72");
                Console.WriteLine(thing.data.username);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getUserCharts()
        {
            Task.Run(async () => {
                var thing = await inst.getUserCharts("72");
                Console.WriteLine(thing.data[0].title);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getUserReviews()
        {
            Task.Run(async () => {
                var thing = await inst.getUserReviews("72");
                Console.WriteLine(thing.data[0].id);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void getUserSpinPlays()
        {
            Task.Run(async () => {
                var thing = await inst.getUserSpinPlays("20");
                Console.WriteLine(thing.data[0].id);
            }).GetAwaiter().GetResult();
        }
        [TestMethod()]
        public void search()
        {
            Task.Run(async () => {
                var thing = await inst.search("ayanamy");
                Console.WriteLine(thing.data.users[0].id);
            }).GetAwaiter().GetResult();
        }
    }
}