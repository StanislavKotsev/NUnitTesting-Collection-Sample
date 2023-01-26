using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.Tests
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var nums = new Collection<int>();
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var nums = new Collection<int>(10, 20, 30);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>(10, 20, 30);
            nums.Add(40);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var names = new Collection<string>("Peter", "Maria");
            names.AddRange("Steve", "Kate", "Jordan");
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria, Steve, Kate, Jordan]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var names = new Collection<string>("Peter", "Maria");
            var item0 = names[0];
            Assert.That(item0, Is.EqualTo("Peter"));
            var item1 = names[1];
            Assert.That(item1, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Peter", "Maria");
            Assert.That(() => { var name = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var names = new Collection<string>("Peter", "Maria");
            names[0] = "Steve";
            names[1] = "Mike";
            Assert.That(names.ToString(), Is.EqualTo("[Steve, Mike]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            var names = new Collection<string>("Peter", "Maria");
            names.InsertAt(0, "Steve");
            Assert.That(names.ToString(), Is.EqualTo("[Steve, Peter, Maria]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            var names = new Collection<string>("Peter", "Maria");
            names.InsertAt(2, "Steve");
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria, Steve]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var names = new Collection<string>("Peter", "Maria");
            names.InsertAt(1, "Steve");
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Steve, Maria]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            var names = new Collection<string>("Peter", "Maria");
            Assert.That(() => names.InsertAt(-1, "Jane"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(3, "Steve"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(500, "Nia"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria]"));
        }
        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            names.Exchange(1, 2);
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Steve, Maria, Mia]"));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            names.Exchange(0, 3);
            Assert.That(names.ToString(), Is.EqualTo("[Mia, Maria, Steve, Peter]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            var names = new Collection<string>("Peter", "Maria");
            Assert.That(() => names.Exchange(-1, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(1, -1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(2, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(1, 2), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(-500, 500), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria]"));
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(0);
            Assert.That(removed, Is.EqualTo("Peter"));
            Assert.That(names.ToString(), Is.EqualTo("[Maria, Steve, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(3);
            Assert.That(removed, Is.EqualTo("Mia"));
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria, Steve]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(1);
            Assert.That(removed, Is.EqualTo("Maria"));
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Steve, Mia]"));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            names.Clear();
            Assert.That(names.Count, Is.EqualTo(0));
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            var names = new Collection<string>();
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            var names = new Collection<string>("Kotsev");
            Assert.That(names.ToString(), Is.EqualTo("[Kotsev]"));
        }



    }

}