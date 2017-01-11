using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Modules.System.Collections.Generic;

namespace Pokemon_3D_Server_Core_Test.Modules.System.Collections.Generic
{
    [TestClass]
    public class DictionaryHelper
    {
        [TestMethod]
        public void MultiThreadedAddTest()
        {
            DictionaryHelper<int, string> test = new DictionaryHelper<int, string>();
            ThreadHelper thread = new ThreadHelper();

            thread.Add(() =>
            {
                test.Add(1, "Test1");
                test.Add(2, "Test1");
                test.Add(3, "Test1");
                test.Add(4, "Test1");
                test.Add(5, "Test1");
                
            });

            thread.Add(() =>
            {
                test.Add(6, "Test1");
                test.Add(7, "Test1");
                test.Add(8, "Test1");
                test.Add(9, "Test1");
                test.Add(10, "Test1");
            });

            thread.Add(() =>
            {
                test.Add(11, "Test1");
                test.Add(12, "Test1");
                test.Add(13, "Test1");
                test.Add(14, "Test1");
                test.Add(15, "Test1");
            });

            thread.Sleep(1000);

            Assert.AreEqual(15, test.Count, test.Keys.Length + " " + string.Join(",", test.Keys));
        }

        [TestMethod]
        public void RemovalTest()
        {
            DictionaryHelper<int, string> test = new DictionaryHelper<int, string>();
            test.Add(1, "Test1");
            test.Add(2, "Test1");
            test.Add(3, "Test1");
            test.Add(4, "Test1");
            test.Add(5, "Test1");
            test.Add(6, "Test1");
            test.Add(7, "Test1");
            test.Add(8, "Test1");
            test.Add(9, "Test1");
            test.Add(10, "Test1");

            int actualLength = test.Keys.Length;

            for (int i = 0; i < actualLength; i++)
                test.Remove(test.Keys[0]);

            Assert.AreEqual(0, test.Count, test.Keys.Length + " " + string.Join(",", test.Keys));
        }
    }
}
