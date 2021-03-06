using NUnit.Framework;
using static Yoga.Net.YGGlobal;



namespace Yoga.Net.Tests
{
    [TestFixture]
    public class YGHadOverflowTest
    {
        [SetUp]
        public void Setup()
        {
            config = YGConfigNew();
            root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 100);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Column);
            YGNodeStyleSetFlexWrap(root, YGWrap.NoWrap);
        }

        [TearDown]
        public void Teardown()
        {
            root = null;
            config = null;
            //
            //
        }

        YGNode root;
        YGConfig config;

    [Test] public void children_overflow_no_wrap_and_no_flex_children() {
        YGNode child0 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child0, 80);
        YGNodeStyleSetHeight(child0, 40);
        YGNodeStyleSetMargin(child0, YGEdge.Top, 10);
        YGNodeStyleSetMargin(child0, YGEdge.Bottom, 15);
        YGNodeInsertChild(root, child0, 0);
        YGNode child1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1, 80);
        YGNodeStyleSetHeight(child1, 40);
        YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
        YGNodeInsertChild(root, child1, 1);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
    }

        [Test]
        public void spacing_overflow_no_wrap_and_no_flex_children() {
        YGNode child0 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child0, 80);
        YGNodeStyleSetHeight(child0, 40);
        YGNodeStyleSetMargin(child0, YGEdge.Top, 10);
        YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
        YGNodeInsertChild(root, child0, 0);
        YGNode child1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1, 80);
        YGNodeStyleSetHeight(child1, 40);
        YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
        YGNodeInsertChild(root, child1, 1);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
    }

    [Test] public void no_overflow_no_wrap_and_flex_children() {
        YGNode child0 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child0, 80);
        YGNodeStyleSetHeight(child0, 40);
        YGNodeStyleSetMargin(child0, YGEdge.Top, 10);
        YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
        YGNodeInsertChild(root, child0, 0);
        YGNode child1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1, 80);
        YGNodeStyleSetHeight(child1, 40);
        YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
        YGNodeStyleSetFlexShrink(child1, 1);
        YGNodeInsertChild(root, child1, 1);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsFalse(YGNodeLayoutGetHadOverflow(root));
    }

    [Test] public void hadOverflow_gets_reset_if_not_logger_valid() {
        YGNode child0 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child0, 80);
        YGNodeStyleSetHeight(child0, 40);
        YGNodeStyleSetMargin(child0, YGEdge.Top, 10);
        YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
        YGNodeInsertChild(root, child0, 0);
        YGNode child1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1, 80);
        YGNodeStyleSetHeight(child1, 40);
        YGNodeStyleSetMargin(child1, YGEdge.Bottom, 5);
        YGNodeInsertChild(root, child1, 1);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));

        YGNodeStyleSetFlexShrink(child1, 1);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsFalse(YGNodeLayoutGetHadOverflow(root));
    }

    [Test] public void spacing_overflow_in_nested_nodes() {
        YGNode child0 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child0, 80);
        YGNodeStyleSetHeight(child0, 40);
        YGNodeStyleSetMargin(child0, YGEdge.Top, 10);
        YGNodeStyleSetMargin(child0, YGEdge.Bottom, 10);
        YGNodeInsertChild(root, child0, 0);
        YGNode child1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1, 80);
        YGNodeStyleSetHeight(child1, 40);
        YGNodeInsertChild(root, child1, 1);
        YGNode child1_1 = YGNodeNewWithConfig(config);
        YGNodeStyleSetWidth(child1_1, 80);
        YGNodeStyleSetHeight(child1_1, 40);
        YGNodeStyleSetMargin(child1_1, YGEdge.Bottom, 5);
        YGNodeInsertChild(child1, child1_1, 0);

        YGNodeCalculateLayout(root, 200, 100, YGDirection.LTR);

        Assert.IsTrue(YGNodeLayoutGetHadOverflow(root));
    }
}
}
