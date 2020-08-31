using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MultiplicationTable.Models;
using MultiplicationTable.Views;
using System.Windows.Input;
using System.Reflection;

namespace MultiplicationTable.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        private int row;
        private int col;

        public ICommand GenerateEquation { protected set; get; }
        public ICommand CheckEquation { protected set; get; }

        public ItemsViewModel()
        {
            Title = "Multiplication table";
            Items = new ObservableCollection<Item>();

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            GenerateEquation = new Command(() =>
            {
                Random r = new Random();
                row = r.Next(1, 10);
                col = r.Next(1, 10);
                Equation = row.ToString() + "x" + col.ToString();

                ClearSquares();
                SetSquares(row, col);
            });

            CheckEquation = new Command(() =>
            {

            });
        }

        private string equation;
        public string Equation
        {
            set
            {
                SetProperty(ref equation, value);
            }
            get
            {
                return equation;
            }
        }

        #region Boring color settings
        private Color _bvColor_1_1;
        public Color bvColor_1_1
        {
            set
            {
                SetProperty(ref _bvColor_1_1, value);
            }
            get
            {
                return _bvColor_1_1;
            }
        }
        private Color _bvColor_1_2;
        public Color bvColor_1_2
        {
            set
            {
                SetProperty(ref _bvColor_1_2, value);
            }
            get
            {
                return _bvColor_1_2;
            }
        }
        private Color _bvColor_1_3;
        public Color bvColor_1_3
        {
            set
            {
                SetProperty(ref _bvColor_1_3, value);
            }
            get
            {
                return _bvColor_1_3;
            }
        }

        private Color _bvColor_1_4;
        public Color bvColor_1_4
        {
            set
            {
                SetProperty(ref _bvColor_1_4, value);
            }
            get
            {
                return _bvColor_1_4;
            }
        }

        private Color _bvColor_1_5;
        public Color bvColor_1_5
        {
            set
            {
                SetProperty(ref _bvColor_1_5, value);
            }
            get
            {
                return _bvColor_1_5;
            }
        }

        private Color _bvColor_1_6;
        public Color bvColor_1_6
        {
            set
            {
                SetProperty(ref _bvColor_1_6, value);
            }
            get
            {
                return _bvColor_1_6;
            }
        }

        private Color _bvColor_1_7;
        public Color bvColor_1_7
        {
            set
            {
                SetProperty(ref _bvColor_1_7, value);
            }
            get
            {
                return _bvColor_1_7;
            }
        }
        private Color _bvColor_1_8;
        public Color bvColor_1_8
        {
            set
            {
                SetProperty(ref _bvColor_1_8, value);
            }
            get
            {
                return _bvColor_1_8;
            }
        }
        private Color _bvColor_1_9;
        public Color bvColor_1_9
        {
            set
            {
                SetProperty(ref _bvColor_1_9, value);
            }
            get
            {
                return _bvColor_1_9;
            }
        }
        private Color _bvColor_1_10;
        public Color bvColor_1_10
        {
            set
            {
                SetProperty(ref _bvColor_1_10, value);
            }
            get
            {
                return _bvColor_1_10;
            }
        }

        private Color _bvColor_2_1;
        public Color bvColor_2_1
        {
            set
            {
                SetProperty(ref _bvColor_2_1, value);
            }
            get
            {
                return _bvColor_2_1;
            }
        }
        private Color _bvColor_2_2;
        public Color bvColor_2_2
        {
            set
            {
                SetProperty(ref _bvColor_2_2, value);
            }
            get
            {
                return _bvColor_2_2;
            }
        }
        private Color _bvColor_2_3;
        public Color bvColor_2_3
        {
            set
            {
                SetProperty(ref _bvColor_2_3, value);
            }
            get
            {
                return _bvColor_2_3;
            }
        }

        private Color _bvColor_2_4;
        public Color bvColor_2_4
        {
            set
            {
                SetProperty(ref _bvColor_2_4, value);
            }
            get
            {
                return _bvColor_2_4;
            }
        }

        private Color _bvColor_2_5;
        public Color bvColor_2_5
        {
            set
            {
                SetProperty(ref _bvColor_2_5, value);
            }
            get
            {
                return _bvColor_2_5;
            }
        }

        private Color _bvColor_2_6;
        public Color bvColor_2_6
        {
            set
            {
                SetProperty(ref _bvColor_2_6, value);
            }
            get
            {
                return _bvColor_2_6;
            }
        }

        private Color _bvColor_2_7;
        public Color bvColor_2_7
        {
            set
            {
                SetProperty(ref _bvColor_2_7, value);
            }
            get
            {
                return _bvColor_2_7;
            }
        }
        private Color _bvColor_2_8;
        public Color bvColor_2_8
        {
            set
            {
                SetProperty(ref _bvColor_2_8, value);
            }
            get
            {
                return _bvColor_2_8;
            }
        }
        private Color _bvColor_2_9;
        public Color bvColor_2_9
        {
            set
            {
                SetProperty(ref _bvColor_2_9, value);
            }
            get
            {
                return _bvColor_2_9;
            }
        }
        private Color _bvColor_2_10;
        public Color bvColor_2_10
        {
            set
            {
                SetProperty(ref _bvColor_2_10, value);
            }
            get
            {
                return _bvColor_2_10;
            }
        }

        private Color _bvColor_3_1;
        public Color bvColor_3_1
        {
            set
            {
                SetProperty(ref _bvColor_3_1, value);
            }
            get
            {
                return _bvColor_3_1;
            }
        }
        private Color _bvColor_3_2;
        public Color bvColor_3_2
        {
            set
            {
                SetProperty(ref _bvColor_3_2, value);
            }
            get
            {
                return _bvColor_3_2;
            }
        }
        private Color _bvColor_3_3;
        public Color bvColor_3_3
        {
            set
            {
                SetProperty(ref _bvColor_3_3, value);
            }
            get
            {
                return _bvColor_3_3;
            }
        }

        private Color _bvColor_3_4;
        public Color bvColor_3_4
        {
            set
            {
                SetProperty(ref _bvColor_3_4, value);
            }
            get
            {
                return _bvColor_3_4;
            }
        }

        private Color _bvColor_3_5;
        public Color bvColor_3_5
        {
            set
            {
                SetProperty(ref _bvColor_3_5, value);
            }
            get
            {
                return _bvColor_3_5;
            }
        }

        private Color _bvColor_3_6;
        public Color bvColor_3_6
        {
            set
            {
                SetProperty(ref _bvColor_3_6, value);
            }
            get
            {
                return _bvColor_3_6;
            }
        }

        private Color _bvColor_3_7;
        public Color bvColor_3_7
        {
            set
            {
                SetProperty(ref _bvColor_3_7, value);
            }
            get
            {
                return _bvColor_3_7;
            }
        }
        private Color _bvColor_3_8;
        public Color bvColor_3_8
        {
            set
            {
                SetProperty(ref _bvColor_3_8, value);
            }
            get
            {
                return _bvColor_3_8;
            }
        }
        private Color _bvColor_3_9;
        public Color bvColor_3_9
        {
            set
            {
                SetProperty(ref _bvColor_3_9, value);
            }
            get
            {
                return _bvColor_3_9;
            }
        }
        private Color _bvColor_3_10;
        public Color bvColor_3_10
        {
            set
            {
                SetProperty(ref _bvColor_3_10, value);
            }
            get
            {
                return _bvColor_3_10;
            }
        }

        private Color _bvColor_4_1;
        public Color bvColor_4_1
        {
            set
            {
                SetProperty(ref _bvColor_4_1, value);
            }
            get
            {
                return _bvColor_4_1;
            }
        }
        private Color _bvColor_4_2;
        public Color bvColor_4_2
        {
            set
            {
                SetProperty(ref _bvColor_4_2, value);
            }
            get
            {
                return _bvColor_4_2;
            }
        }
        private Color _bvColor_4_3;
        public Color bvColor_4_3
        {
            set
            {
                SetProperty(ref _bvColor_4_3, value);
            }
            get
            {
                return _bvColor_4_3;
            }
        }

        private Color _bvColor_4_4;
        public Color bvColor_4_4
        {
            set
            {
                SetProperty(ref _bvColor_4_4, value);
            }
            get
            {
                return _bvColor_4_4;
            }
        }

        private Color _bvColor_4_5;
        public Color bvColor_4_5
        {
            set
            {
                SetProperty(ref _bvColor_4_5, value);
            }
            get
            {
                return _bvColor_4_5;
            }
        }

        private Color _bvColor_4_6;
        public Color bvColor_4_6
        {
            set
            {
                SetProperty(ref _bvColor_4_6, value);
            }
            get
            {
                return _bvColor_4_6;
            }
        }

        private Color _bvColor_4_7;
        public Color bvColor_4_7
        {
            set
            {
                SetProperty(ref _bvColor_4_7, value);
            }
            get
            {
                return _bvColor_4_7;
            }
        }
        private Color _bvColor_4_8;
        public Color bvColor_4_8
        {
            set
            {
                SetProperty(ref _bvColor_4_8, value);
            }
            get
            {
                return _bvColor_4_8;
            }
        }
        private Color _bvColor_4_9;
        public Color bvColor_4_9
        {
            set
            {
                SetProperty(ref _bvColor_4_9, value);
            }
            get
            {
                return _bvColor_4_9;
            }
        }
        private Color _bvColor_4_10;
        public Color bvColor_4_10
        {
            set
            {
                SetProperty(ref _bvColor_4_10, value);
            }
            get
            {
                return _bvColor_4_10;
            }
        }

        private Color _bvColor_5_1;
        public Color bvColor_5_1
        {
            set
            {
                SetProperty(ref _bvColor_5_1, value);
            }
            get
            {
                return _bvColor_5_1;
            }
        }
        private Color _bvColor_5_2;
        public Color bvColor_5_2
        {
            set
            {
                SetProperty(ref _bvColor_5_2, value);
            }
            get
            {
                return _bvColor_5_2;
            }
        }
        private Color _bvColor_5_3;
        public Color bvColor_5_3
        {
            set
            {
                SetProperty(ref _bvColor_5_3, value);
            }
            get
            {
                return _bvColor_5_3;
            }
        }

        private Color _bvColor_5_4;
        public Color bvColor_5_4
        {
            set
            {
                SetProperty(ref _bvColor_5_4, value);
            }
            get
            {
                return _bvColor_5_4;
            }
        }

        private Color _bvColor_5_5;
        public Color bvColor_5_5
        {
            set
            {
                SetProperty(ref _bvColor_5_5, value);
            }
            get
            {
                return _bvColor_5_5;
            }
        }

        private Color _bvColor_5_6;
        public Color bvColor_5_6
        {
            set
            {
                SetProperty(ref _bvColor_5_6, value);
            }
            get
            {
                return _bvColor_5_6;
            }
        }

        private Color _bvColor_5_7;
        public Color bvColor_5_7
        {
            set
            {
                SetProperty(ref _bvColor_5_7, value);
            }
            get
            {
                return _bvColor_5_7;
            }
        }
        private Color _bvColor_5_8;
        public Color bvColor_5_8
        {
            set
            {
                SetProperty(ref _bvColor_5_8, value);
            }
            get
            {
                return _bvColor_5_8;
            }
        }
        private Color _bvColor_5_9;
        public Color bvColor_5_9
        {
            set
            {
                SetProperty(ref _bvColor_5_9, value);
            }
            get
            {
                return _bvColor_5_9;
            }
        }
        private Color _bvColor_5_10;
        public Color bvColor_5_10
        {
            set
            {
                SetProperty(ref _bvColor_5_10, value);
            }
            get
            {
                return _bvColor_5_10;
            }
        }

        private Color _bvColor_6_1;
        public Color bvColor_6_1
        {
            set
            {
                SetProperty(ref _bvColor_6_1, value);
            }
            get
            {
                return _bvColor_6_1;
            }
        }
        private Color _bvColor_6_2;
        public Color bvColor_6_2
        {
            set
            {
                SetProperty(ref _bvColor_6_2, value);
            }
            get
            {
                return _bvColor_6_2;
            }
        }
        private Color _bvColor_6_3;
        public Color bvColor_6_3
        {
            set
            {
                SetProperty(ref _bvColor_6_3, value);
            }
            get
            {
                return _bvColor_6_3;
            }
        }

        private Color _bvColor_6_4;
        public Color bvColor_6_4
        {
            set
            {
                SetProperty(ref _bvColor_6_4, value);
            }
            get
            {
                return _bvColor_6_4;
            }
        }

        private Color _bvColor_6_5;
        public Color bvColor_6_5
        {
            set
            {
                SetProperty(ref _bvColor_6_5, value);
            }
            get
            {
                return _bvColor_6_5;
            }
        }

        private Color _bvColor_6_6;
        public Color bvColor_6_6
        {
            set
            {
                SetProperty(ref _bvColor_6_6, value);
            }
            get
            {
                return _bvColor_6_6;
            }
        }

        private Color _bvColor_6_7;
        public Color bvColor_6_7
        {
            set
            {
                SetProperty(ref _bvColor_6_7, value);
            }
            get
            {
                return _bvColor_6_7;
            }
        }
        private Color _bvColor_6_8;
        public Color bvColor_6_8
        {
            set
            {
                SetProperty(ref _bvColor_6_8, value);
            }
            get
            {
                return _bvColor_6_8;
            }
        }
        private Color _bvColor_6_9;
        public Color bvColor_6_9
        {
            set
            {
                SetProperty(ref _bvColor_6_9, value);
            }
            get
            {
                return _bvColor_6_9;
            }
        }
        private Color _bvColor_6_10;
        public Color bvColor_6_10
        {
            set
            {
                SetProperty(ref _bvColor_6_10, value);
            }
            get
            {
                return _bvColor_6_10;
            }
        }

        private Color _bvColor_7_1;
        public Color bvColor_7_1
        {
            set
            {
                SetProperty(ref _bvColor_7_1, value);
            }
            get
            {
                return _bvColor_7_1;
            }
        }
        private Color _bvColor_7_2;
        public Color bvColor_7_2
        {
            set
            {
                SetProperty(ref _bvColor_7_2, value);
            }
            get
            {
                return _bvColor_7_2;
            }
        }
        private Color _bvColor_7_3;
        public Color bvColor_7_3
        {
            set
            {
                SetProperty(ref _bvColor_7_3, value);
            }
            get
            {
                return _bvColor_7_3;
            }
        }

        private Color _bvColor_7_4;
        public Color bvColor_7_4
        {
            set
            {
                SetProperty(ref _bvColor_7_4, value);
            }
            get
            {
                return _bvColor_7_4;
            }
        }

        private Color _bvColor_7_5;
        public Color bvColor_7_5
        {
            set
            {
                SetProperty(ref _bvColor_7_5, value);
            }
            get
            {
                return _bvColor_7_5;
            }
        }

        private Color _bvColor_7_6;
        public Color bvColor_7_6
        {
            set
            {
                SetProperty(ref _bvColor_7_6, value);
            }
            get
            {
                return _bvColor_7_6;
            }
        }

        private Color _bvColor_7_7;
        public Color bvColor_7_7
        {
            set
            {
                SetProperty(ref _bvColor_7_7, value);
            }
            get
            {
                return _bvColor_7_7;
            }
        }
        private Color _bvColor_7_8;
        public Color bvColor_7_8
        {
            set
            {
                SetProperty(ref _bvColor_7_8, value);
            }
            get
            {
                return _bvColor_7_8;
            }
        }
        private Color _bvColor_7_9;
        public Color bvColor_7_9
        {
            set
            {
                SetProperty(ref _bvColor_7_9, value);
            }
            get
            {
                return _bvColor_7_9;
            }
        }
        private Color _bvColor_7_10;
        public Color bvColor_7_10
        {
            set
            {
                SetProperty(ref _bvColor_7_10, value);
            }
            get
            {
                return _bvColor_7_10;
            }
        }

        private Color _bvColor_8_1;
        public Color bvColor_8_1
        {
            set
            {
                SetProperty(ref _bvColor_8_1, value);
            }
            get
            {
                return _bvColor_8_1;
            }
        }
        private Color _bvColor_8_2;
        public Color bvColor_8_2
        {
            set
            {
                SetProperty(ref _bvColor_8_2, value);
            }
            get
            {
                return _bvColor_8_2;
            }
        }
        private Color _bvColor_8_3;
        public Color bvColor_8_3
        {
            set
            {
                SetProperty(ref _bvColor_8_3, value);
            }
            get
            {
                return _bvColor_8_3;
            }
        }

        private Color _bvColor_8_4;
        public Color bvColor_8_4
        {
            set
            {
                SetProperty(ref _bvColor_8_4, value);
            }
            get
            {
                return _bvColor_8_4;
            }
        }

        private Color _bvColor_8_5;
        public Color bvColor_8_5
        {
            set
            {
                SetProperty(ref _bvColor_8_5, value);
            }
            get
            {
                return _bvColor_8_5;
            }
        }

        private Color _bvColor_8_6;
        public Color bvColor_8_6
        {
            set
            {
                SetProperty(ref _bvColor_8_6, value);
            }
            get
            {
                return _bvColor_8_6;
            }
        }

        private Color _bvColor_8_7;
        public Color bvColor_8_7
        {
            set
            {
                SetProperty(ref _bvColor_8_7, value);
            }
            get
            {
                return _bvColor_8_7;
            }
        }
        private Color _bvColor_8_8;
        public Color bvColor_8_8
        {
            set
            {
                SetProperty(ref _bvColor_8_8, value);
            }
            get
            {
                return _bvColor_8_8;
            }
        }
        private Color _bvColor_8_9;
        public Color bvColor_8_9
        {
            set
            {
                SetProperty(ref _bvColor_8_9, value);
            }
            get
            {
                return _bvColor_8_9;
            }
        }
        private Color _bvColor_8_10;
        public Color bvColor_8_10
        {
            set
            {
                SetProperty(ref _bvColor_8_10, value);
            }
            get
            {
                return _bvColor_8_10;
            }
        }

        private Color _bvColor_9_1;
        public Color bvColor_9_1
        {
            set
            {
                SetProperty(ref _bvColor_9_1, value);
            }
            get
            {
                return _bvColor_9_1;
            }
        }
        private Color _bvColor_9_2;
        public Color bvColor_9_2
        {
            set
            {
                SetProperty(ref _bvColor_9_2, value);
            }
            get
            {
                return _bvColor_9_2;
            }
        }
        private Color _bvColor_9_3;
        public Color bvColor_9_3
        {
            set
            {
                SetProperty(ref _bvColor_9_3, value);
            }
            get
            {
                return _bvColor_9_3;
            }
        }

        private Color _bvColor_9_4;
        public Color bvColor_9_4
        {
            set
            {
                SetProperty(ref _bvColor_9_4, value);
            }
            get
            {
                return _bvColor_9_4;
            }
        }

        private Color _bvColor_9_5;
        public Color bvColor_9_5
        {
            set
            {
                SetProperty(ref _bvColor_9_5, value);
            }
            get
            {
                return _bvColor_9_5;
            }
        }

        private Color _bvColor_9_6;
        public Color bvColor_9_6
        {
            set
            {
                SetProperty(ref _bvColor_9_6, value);
            }
            get
            {
                return _bvColor_9_6;
            }
        }

        private Color _bvColor_9_7;
        public Color bvColor_9_7
        {
            set
            {
                SetProperty(ref _bvColor_9_7, value);
            }
            get
            {
                return _bvColor_9_7;
            }
        }
        private Color _bvColor_9_8;
        public Color bvColor_9_8
        {
            set
            {
                SetProperty(ref _bvColor_9_8, value);
            }
            get
            {
                return _bvColor_9_8;
            }
        }
        private Color _bvColor_9_9;
        public Color bvColor_9_9
        {
            set
            {
                SetProperty(ref _bvColor_9_9, value);
            }
            get
            {
                return _bvColor_9_9;
            }
        }
        private Color _bvColor_9_10;
        public Color bvColor_9_10
        {
            set
            {
                SetProperty(ref _bvColor_9_10, value);
            }
            get
            {
                return _bvColor_9_10;
            }
        }

        private Color _bvColor_10_1;
        public Color bvColor_10_1
        {
            set
            {
                SetProperty(ref _bvColor_10_1, value);
            }
            get
            {
                return _bvColor_10_1;
            }
        }
        private Color _bvColor_10_2;
        public Color bvColor_10_2
        {
            set
            {
                SetProperty(ref _bvColor_10_2, value);
            }
            get
            {
                return _bvColor_10_2;
            }
        }
        private Color _bvColor_10_3;
        public Color bvColor_10_3
        {
            set
            {
                SetProperty(ref _bvColor_10_3, value);
            }
            get
            {
                return _bvColor_10_3;
            }
        }

        private Color _bvColor_10_4;
        public Color bvColor_10_4
        {
            set
            {
                SetProperty(ref _bvColor_10_4, value);
            }
            get
            {
                return _bvColor_10_4;
            }
        }

        private Color _bvColor_10_5;
        public Color bvColor_10_5
        {
            set
            {
                SetProperty(ref _bvColor_10_5, value);
            }
            get
            {
                return _bvColor_10_5;
            }
        }

        private Color _bvColor_10_6;
        public Color bvColor_10_6
        {
            set
            {
                SetProperty(ref _bvColor_10_6, value);
            }
            get
            {
                return _bvColor_10_6;
            }
        }

        private Color _bvColor_10_7;
        public Color bvColor_10_7
        {
            set
            {
                SetProperty(ref _bvColor_10_7, value);
            }
            get
            {
                return _bvColor_10_7;
            }
        }
        private Color _bvColor_10_8;
        public Color bvColor_10_8
        {
            set
            {
                SetProperty(ref _bvColor_10_8, value);
            }
            get
            {
                return _bvColor_10_8;
            }
        }
        private Color _bvColor_10_9;
        public Color bvColor_10_9
        {
            set
            {
                SetProperty(ref _bvColor_10_9, value);
            }
            get
            {
                return _bvColor_10_9;
            }
        }
        private Color _bvColor_10_10;
        public Color bvColor_10_10
        {
            set
            {
                SetProperty(ref _bvColor_10_10, value);
            }
            get
            {
                return _bvColor_10_10;
            }
        }

        #endregion

        private void ClearSquares()
        {
            for(int r = 1;r<=10;r++)
            {
                for(int c=1;c<=10;c++)
                {
                     string name = "bvColor_" + r.ToString() + "_" + c.ToString();
                     PropertyInfo pi = this.GetType().GetProperty(name);
                    if(pi!=null)
                    {
                        //Color clr = new Color();

                        pi.SetValue(this,Color.Green);
                    }
                }
            }
        }

        private void SetSquares(int rMax, int cMax)
        {
            for (int r = 1; r <= rMax; r++)
            {
                for (int c = 1; c <= cMax; c++)
                {
                    string name = "bvColor_" + r.ToString() + "_" + c.ToString();
                    PropertyInfo pi = this.GetType().GetProperty(name);
                    if (pi != null)
                    {
                        //Color clr = new Color();

                        pi.SetValue(this, Color.Red);
                    }
                }
            }
        }

    }
}