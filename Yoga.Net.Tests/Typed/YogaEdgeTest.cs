using NUnit.Framework;

namespace Yoga.Net.Tests.Typed
{
    [TestFixture]
    public class YogaEdgeTest
    {
        [Test]
        public void start_overrides()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Start, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Left, 20);
            YGNodeStyleSetMargin(root_child0, Edge.Right, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(10, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(20, YGNodeLayoutGetRight(root_child0));

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.RTL);
            Assert.AreEqual(20, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetRight(root_child0));
        }

        [Test]
        public void end_overrides()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.End, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Left, 20);
            YGNodeStyleSetMargin(root_child0, Edge.Right, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(20, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetRight(root_child0));

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.RTL);
            Assert.AreEqual(10, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(20, YGNodeLayoutGetRight(root_child0));
        }

        [Test]
        public void horizontal_overridden()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Row);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Horizontal, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Left, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(20, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetRight(root_child0));
        }

        [Test]
        public void vertical_overridden()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Vertical, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Top, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(20, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetBottom(root_child0));
        }

        [Test]
        public void horizontal_overrides_all()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Horizontal, 10);
            YGNodeStyleSetMargin(root_child0, Edge.All, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(10, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(20, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetRight(root_child0));
            Assert.AreEqual(20, YGNodeLayoutGetBottom(root_child0));
        }

        [Test]
        public void vertical_overrides_all()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Vertical, 10);
            YGNodeStyleSetMargin(root_child0, Edge.All, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(20, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(20, YGNodeLayoutGetRight(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetBottom(root_child0));
        }

        [Test]
        public void all_overridden()
        {
            YogaNode root = YGNodeNew();
            YGNodeStyleSetFlexDirection(root, FlexDirection.Column);
            YGNodeStyleSetWidth(root, 100);
            YGNodeStyleSetHeight(root, 100);

            YogaNode root_child0 = YGNodeNew();
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMargin(root_child0, Edge.Left, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Top, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Right, 10);
            YGNodeStyleSetMargin(root_child0, Edge.Bottom, 10);
            YGNodeStyleSetMargin(root_child0, Edge.All, 20);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);
            Assert.AreEqual(10, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetRight(root_child0));
            Assert.AreEqual(10, YGNodeLayoutGetBottom(root_child0));
        }
    }
}