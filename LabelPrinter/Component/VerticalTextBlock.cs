﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace LabelPrinter.Component
{
    public class VerticalTextBlock : ContentControl
    {
        public VerticalTextBlock()
        {
            IsTabStop = false;
            var templateXaml =
                @"<ControlTemplate " +
#if SILVERLIGHT
 "xmlns='http://schemas.microsoft.com/client/2007' " +
#else
 "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
#endif
 "xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>" +
                    "<Grid Background=\"{TemplateBinding Background}\">" +
                        "<TextBlock x:Name=\"TextBlock\" TextAlignment=\"Center\"/>" +
                    "</Grid>" +
                "</ControlTemplate>";
#if SILVERLIGHT
            Template = (ControlTemplate)XamlReader.Load(templateXaml);
#else
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(templateXaml)))
            {
                Template = (ControlTemplate)XamlReader.Load(stream);
            }
#endif
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _textBlock = GetTemplateChild("TextBlock") as TextBlock;
            CreateVerticalText(_text);
        }

        private string _text { get; set; }
        private TextBlock _textBlock { get; set; }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(VerticalTextBlock), new PropertyMetadata(OnTextChanged));
        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((VerticalTextBlock)o).OnTextChanged((string)(e.NewValue));
        }
        private void OnTextChanged(string newValue)
        {
            CreateVerticalText(newValue);
        }

        private void CreateVerticalText(string text)
        {
            _text = text;
            if (null != _textBlock)
            {
                _textBlock.Inlines.Clear();
                bool first = true;
                foreach (var c in _text)
                {
                    if (!first)
                    {
                        _textBlock.Inlines.Add(new LineBreak());
                    }
                    _textBlock.Inlines.Add(new Run { Text = c.ToString() });
                    first = false;
                }
            }
        }
    }
}
