using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using GitCafeCommon.Models;

namespace GitCafeModule.WorkSpace.ValueConverter
{
    public class CommitIdConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converterValue = value as ObjectId;
            if (converterValue != null)
            {
                return converterValue.ToString(7);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommitIdFullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converterValue = value as ObjectId;
            if (converterValue != null)
            {
                return string.Format("{0}[{1}]", converterValue.ToString(), converterValue.ToString(7));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommitParentIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var commit = value as Commit;
            if (commit != null)
            {
                if (commit.Parents != null)
                {
                    try
                    {
                        return commit.Parents.Single().Id.ToString(7);
                    }
                    catch { }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommitTreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var commit = value as Commit;
            if (commit != null)
            {
                //var ltResult = new List<TreeEntry>();
                //var trees = commit.Tree.GetEnumerator();
                //while (trees.MoveNext())
                //{
                //    ltResult.Add(trees.Current);
                //}
                //return ltResult;
                var repo = parameter as GitCafeRepository;
                var tree = repo.Repository.Lookup<Tree>(commit.Id);
                foreach (var item in tree)
                {
                    MessageBox.Show(item.Name);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
