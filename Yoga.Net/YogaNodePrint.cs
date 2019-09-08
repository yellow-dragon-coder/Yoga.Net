﻿using System.Diagnostics;
using System.Text;
using static Yoga.Net.YogaMath;

namespace Yoga.Net
{
    public class YogaNodePrint
    {
        StringBuilder _sb;
        PrintOptions _options;
        public static YogaNode DefaultYogaNode { get; } = new YogaNode();

        public YogaNodePrint(PrintOptions printOptions, StringBuilder sb = null)
        {
            _options = printOptions;
            _sb = sb ?? new StringBuilder();
        }

        void Indent(int level)
        {
            for (int i = 0; i < level; ++i)
                _sb.Append("  ");
        }

        bool AreFourValuesEqual(EdgesReadonly four)
        {
            return
                four[Edge.Left] == four[Edge.Top] &&
                four[Edge.Left] == four[Edge.Right] &&
                four[Edge.Left] == four[Edge.Bottom];
        }

        void AppendString(string str) => _sb.Append(str);

        void AppendFloatOptionalIfDefined(
            string key,
            float num)
        {
            if (num.HasValue())
                _sb.Append($"{key}: {num:G}; ");
        }

        void AppendNumberIfNotUndefined(
            string key,
            YogaValue number)
        {
            if (number.Unit == YogaUnit.Undefined)
                return;

            if (number.Unit == YogaUnit.Auto)
                _sb.Append($"{key}: auto; ");
            else
                _sb.Append($"{key}: {number}; ");
        }

        void AppendNumberIfNotAuto(
            string key,
            YogaValue number)
        {
            if (number.Unit != YogaUnit.Auto)
                AppendNumberIfNotUndefined(key, number);
        }

        void AppendNumberIfNotZero(
            string str,
            YogaValue number)
        {
            if (number.Unit == YogaUnit.Auto)
            {
                _sb.Append(str + ": auto; ");
            }
            else if (!FloatsEqual(number.Value, 0))
            {
                AppendNumberIfNotUndefined(str, number);
            }
        }

        void AppendEdges(string key, EdgesReadonly edges)
        {
            if (AreFourValuesEqual(edges))
            {
                AppendNumberIfNotZero(key, edges[Edge.Left]);
            }
            else
            {
                for (var edge = Edge.Left; edge != Edge.All; ++edge)
                {
                    string str = key + "-" + (edge.ToString().ToLower());
                    AppendNumberIfNotZero(str, edges[edge]);
                }
            }
        }

        void AppendEdgeIfNotUndefined(string str, EdgesReadonly edges, Edge edge)
        {
            AppendNumberIfNotUndefined(str, edges.ComputedEdgeValue(edge, YogaValue.Undefined));
        }

        public YogaNodePrint Output(YogaNode node, int level = 0)
        {
            Indent(level);
            AppendString("<div ");

            if (!string.IsNullOrWhiteSpace(node.Trace))
                AppendString(node.Trace + "; ");

            if (_options.HasFlag(PrintOptions.Layout))
            {
                AppendString("layout=\"");
                AppendString($"width: {node.Layout.Width:G};");
                AppendString($" height: {node.Layout.Height:G};");
                AppendString($" top: {node.Layout.Position[Edge.Top]:G};");
                AppendString($" left: {node.Layout.Position[(int)Edge.Left]:G};");
                if (!node.Layout.Margin.IsZero)
                    AppendString($" margin: {node.Layout.Margin};");
                if (!node.Layout.Border.IsZero)
                    AppendString($" border: {node.Layout.Border};");
                if (!node.Layout.Padding.IsZero)
                    AppendString($" padding: {node.Layout.Padding};");
                AppendString("\" ");
            }

            if (_options.HasFlag(PrintOptions.Style))
            {
                AppendString("style=\"");
                if (node.StyleFlexDirection != DefaultYogaNode.StyleFlexDirection)
                {
                    AppendString($"flex-direction: {node.StyleFlexDirection.ToString().ToLower()}; ");
                }

                if (node.StyleJustifyContent != DefaultYogaNode.StyleJustifyContent)
                {
                    AppendString($"justify-content: {node.StyleJustifyContent.ToString().ToLower()}; ");
                }

                if (node.StyleAlignItems != DefaultYogaNode.StyleAlignItems)
                {
                    AppendString($"align-items: {node.StyleAlignItems.ToString().ToLower()}; ");
                }

                if (node.StyleAlignContent != DefaultYogaNode.StyleAlignContent)
                {
                    AppendString($"align-content: {node.StyleAlignContent.ToString().ToLower()}; ");
                }

                if (node.StyleAlignSelf != DefaultYogaNode.StyleAlignSelf)
                {
                    AppendString($"align-self: {node.StyleAlignSelf.ToString().ToLower()}; ");
                }

                AppendFloatOptionalIfDefined("flex-grow", node.StyleReadonly.FlexGrow);
                AppendFloatOptionalIfDefined("flex-shrink", node.StyleReadonly.FlexShrink);
                AppendNumberIfNotAuto("flex-basis", node.StyleFlexBasis);
                AppendFloatOptionalIfDefined("flex", node.StyleReadonly.Flex);

                if (node.StyleFlexWrap != DefaultYogaNode.StyleFlexWrap)
                {
                    AppendString($"flex-wrap: {node.StyleFlexWrap.ToString().ToLower()}; ");
                }

                if (node.StyleOverflow != DefaultYogaNode.StyleOverflow)
                {
                    AppendString($"overflow: {node.StyleOverflow.ToString().ToLower()}; ");
                }

                if (node.StyleDisplay != DefaultYogaNode.StyleDisplay)
                {
                    AppendString($"display: {node.StyleDisplay.ToString().ToLower()}; ");
                }

                AppendEdges("margin", node.StyleMargin);
                AppendEdges("padding", node.StylePadding);
                AppendEdges("border", node.StyleBorder);

                AppendNumberIfNotAuto("width", node.StyleWidth);
                AppendNumberIfNotAuto("height", node.StyleHeight);
                AppendNumberIfNotAuto("max-width", node.StyleMaxWidth);
                AppendNumberIfNotAuto("max-height", node.StyleMaxHeight);
                AppendNumberIfNotAuto("min-width", node.StyleMinWidth);
                AppendNumberIfNotAuto("min-height", node.StyleMinHeight);

                if (node.StylePositionType != DefaultYogaNode.StylePositionType)
                {
                    AppendString($"position: {node.StylePositionType.ToString().ToLower()}; ");
                }

                AppendEdgeIfNotUndefined("left", node.StylePosition, Edge.Left);
                AppendEdgeIfNotUndefined("right", node.StylePosition, Edge.Right);
                AppendEdgeIfNotUndefined("top", node.StylePosition, Edge.Top);
                AppendEdgeIfNotUndefined("bottom", node.StylePosition, Edge.Bottom);
                AppendString("\" ");

                if (node.HasMeasureFunc)
                {
                    AppendString("has-custom-measure=\"true\"");
                }
            }

            AppendString(">");

            var childCount = node.Children.Count;
            if (_options.HasFlag(PrintOptions.Children) && childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    AppendString("\n");
                    Output(node.Children[i], level + 1);
                }

                AppendString("\n");
                Indent(level);
            }

            AppendString("</div>");

            return this;
        }

        [Conditional("DEBUG")]
        public static void Output(string message, YogaNode node, PrintOptions options = PrintOptions.Layout | PrintOptions.Style | PrintOptions.Children)
        {
            var print = new YogaNodePrint(options);
            print.Output(node);
            Logger.Log(LogLevel.Debug, "\n" + message);
            Logger.Log(LogLevel.Debug, print.ToString());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}
