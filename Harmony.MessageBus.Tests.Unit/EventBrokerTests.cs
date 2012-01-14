//using NUnit.Framework;


//namespace Tally.Bus.Tests.Unit
//{
//    [TestFixture()]
//    public class EventBrokerTests
//    {

//        //[Test()]
//        //public void TestItNow()
//        //{

//        //    var fooHandlerOne = new FooHandler(1);
//        //    var fooHandlerTwo = new FooHandler(2);
//        //    var fooHandlerThree = new FooHandler(3);

//        //    MessageBrokerage.SetInstantiator(() => new MessageBrokerage());

//        //    MessageBrokerage.Instance.AddHandler(fooHandlerOne);
//        //    Console.WriteLine("Added Handler 1");
//        //    Console.WriteLine("One Handler Receiving 5 Events:");
//        //    for (var i = 1; i <= 5; i++)
//        //    {
//        //        var id = Guid.NewGuid();
//        //        MessageBrokerage.Instance.Raise(new FooHappens(id));
//        //    }
//        //    MessageBrokerage.Instance.RemoveHandler(fooHandlerOne);
//        //    Console.WriteLine("Removed Handler 1");
//        //    Console.WriteLine();
//        //    Console.WriteLine("No Handlers Receiving 5 Events:");
//        //    for (var i = 1; i <= 5; i++)
//        //    {
//        //        var id = Guid.NewGuid();
//        //        Console.WriteLine("Raising Event {0}", id);
//        //        MessageBrokerage.Instance.Raise(new FooHappens(id));
//        //    }
//        //    Console.WriteLine("Hopefully No Handlers Notified!");
//        //    Console.WriteLine();

//        //    MessageBrokerage.Instance.AddHandler(fooHandlerOne);
//        //    Console.WriteLine("Added Handler 1");
//        //    MessageBrokerage.Instance.AddHandler(fooHandlerTwo);
//        //    Console.WriteLine("Added Handler 2");
//        //    MessageBrokerage.Instance.AddHandler(fooHandlerThree);
//        //    Console.WriteLine("Added Handler 3");
//        //    Console.WriteLine("Three Handlers Receiving 5 Events:");
//        //    for (var i = 1; i <= 5; i++)
//        //    {
//        //        var id = Guid.NewGuid();
//        //        MessageBrokerage.Instance.Raise(new FooHappens(id));
//        //    }
//        //    MessageBrokerage.Instance.RemoveHandler(fooHandlerOne);
//        //    Console.WriteLine("Removed Handler 1");
//        //    MessageBrokerage.Instance.RemoveHandler(fooHandlerTwo);
//        //    Console.WriteLine("Removed Handler 2");
//        //    MessageBrokerage.Instance.RemoveHandler(fooHandlerThree);
//        //    Console.WriteLine("Removed Handler 3");
//        //    Console.WriteLine();

//        //    MessageBrokerage.Instance.AddHandler(fooHandlerOne);
//        //    Console.WriteLine("Added Handler 1 Once");
//        //    MessageBrokerage.Instance.AddHandler(fooHandlerOne);
//        //    Console.WriteLine("Added Handler 1 Twice");
//        //    MessageBrokerage.Instance.AddHandler(fooHandlerOne);
//        //    Console.WriteLine("Added Handler 1 Thrice");
//        //    Console.WriteLine("One Handler Receiving 5 Events 3 Different Times:");
//        //    for (var i = 1; i <= 5; i++)
//        //    {
//        //        var id = Guid.NewGuid();
//        //        MessageBrokerage.Instance.Raise(new FooHappens(id));
//        //    }

//        //}

//    }
//}