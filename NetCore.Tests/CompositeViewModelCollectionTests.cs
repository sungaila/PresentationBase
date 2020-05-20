using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace PresentationBase.Tests
{
    [TestClass]
    public class CompositeViewModelCollectionTests
    {
        [TestMethod]
        public void Exceptions()
        {
            var viewModel = new TestViewModel();

            Assert.ThrowsException<ArgumentNullException>(() => viewModel.Composition.Add(null!));
            Assert.ThrowsException<ArgumentNullException>(() => viewModel.Composition.Contains(null!));
            Assert.ThrowsException<ArgumentNullException>(() => viewModel.Composition.IndexOf(null!));
            Assert.ThrowsException<ArgumentNullException>(() => viewModel.Composition.Insert(0, null!));
            Assert.ThrowsException<ArgumentNullException>(() => viewModel.Composition.Remove(null!));
        }

        [TestMethod]
        public void CollectionManipulation()
        {
            var viewModel = new TestViewModel();
            Assert.IsNotNull(viewModel.Composition);
            Assert.AreEqual(2, viewModel.Composition.Count);
            Assert.AreSame(viewModel.Children, viewModel.Composition[0]);
            Assert.AreSame(viewModel.Dummies, viewModel.Composition[1]);

            foreach (var collection in viewModel.Composition)
            {
                Assert.IsNotNull(collection);
                Assert.AreEqual(0, collection.Count());
            }

            viewModel.Children!.AddRange(new[] { new TestViewModel(), new TestViewModel(), new TestViewModel() });
            Assert.AreEqual(2, viewModel.Composition.Count);
            Assert.AreEqual(3, viewModel.Composition[0].Count());
            viewModel.Children.Clear();
            Assert.AreEqual(0, viewModel.Composition[0].Count());

            viewModel.Dummies!.AddRange(new[] { new DummyViewModel() });
            Assert.AreEqual(2, viewModel.Composition.Count);
            Assert.AreEqual(1, viewModel.Composition[1].Count());
            viewModel.Dummies.Clear();
            Assert.AreEqual(0, viewModel.Composition[1].Count());

            viewModel.Composition.Remove(viewModel.Children);
            Assert.AreEqual(1, viewModel.Composition.Count);
            Assert.AreSame(viewModel.Dummies, viewModel.Composition[0]);

            viewModel.Composition.Clear();
            Assert.AreEqual(0, viewModel.Composition.Count);
        }

        class TestViewModel : ViewModel
        {
            public ObservableViewModelCollection<TestViewModel>? Children { get; set; }

            public ObservableViewModelCollection<DummyViewModel>? Dummies { get; set; }

            public CompositeViewModelCollection<ViewModel> Composition { get; set; } = new CompositeViewModelCollection<ViewModel>();

            public TestViewModel()
            {
                Children = new ObservableViewModelCollection<TestViewModel>(this);
                Dummies = new ObservableViewModelCollection<DummyViewModel>(this);
                Composition.Add(Children);
                Composition.Add(Dummies);
            }
        }

        class DummyViewModel : ViewModel { }
    }
}
