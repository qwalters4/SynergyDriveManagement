﻿using IM.Services;
using IM.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IM.Models;

namespace IM.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        private InventoryModel model;
        private ObservableCollection<InventoryItem> ui_inventory;
        private InventoryItem selectedItem;
        private List<string> possibleformfactors = new List<string>{ "2.5", "3.5", "M.2","test" };
        private List<KeyValuePair<string, bool>> activeFormFactorFilter = new List<KeyValuePair<string, bool>>();
        private string updateMessage = "Changes have been made, please save the results.";
        private bool saveRequired;

        public ObservableCollection<InventoryItem> UI_Inventory { get => ui_inventory; set => ui_inventory = value; }
        public InventoryItem SelectedItem { get => selectedItem; set => selectedItem = value; }
        public List<string> PossibleFormFactors { get => possibleformfactors; set => possibleformfactors = value; }
        public List<KeyValuePair<string, bool>> ActiveFFFilter { get => activeFormFactorFilter; set => activeFormFactorFilter = value; }
        public bool SaveRequired
        {
            get => saveRequired;
            set { saveRequired = value; OnPropertyChanged(nameof(SaveRequired)); }
        }
        public string UpdateMessage
        { 
            get => updateMessage; 
            set
            {
                updateMessage = value;
                OnPropertyChanged(nameof(UpdateMessage));
            } 
        }

        public InventoryViewModel()
        {
            ui_inventory = new ObservableCollection<InventoryItem>();
            testload();
            SetupChangeListeners();
            SetSaveRequired();

            KeyValuePair<string, bool> a = new KeyValuePair<string, bool>("a", false);
            KeyValuePair<string, bool> b = new KeyValuePair<string, bool>("b", false);
            KeyValuePair<string, bool> c = new KeyValuePair<string, bool>("c", true);
            KeyValuePair<string, bool> d = new KeyValuePair<string, bool>("d", false);

            ActiveFFFilter.Add(a);
            ActiveFFFilter.Add(b);
            ActiveFFFilter.Add(c);
            ActiveFFFilter.Add(d);
        }

        //public async task Refresh()
        //{
        //    UI_Inventory = null;

        //}

        public void testload()
        {
            UI_Inventory = new ObservableCollection<InventoryItem>();

            InventoryItem item1 = new InventoryItem();
            item1.ModelID = "first";
            item1.DiskInterface = "sata";
            item1.FormFactor = "2.5\"";
            item1.RotationRate = "7200rpm";
            item1.SectorSize = 512;
            UI_Inventory.Add(item1);

            InventoryItem item2 = new InventoryItem();
            item2.ModelID = "second";
            item2.DiskInterface = "sata";
            item2.FormFactor = "2.5\"";
            item2.RotationRate = "5400rpm";
            item2.SectorSize = 512;
            UI_Inventory.Add(item2);

            InventoryItem item3 = new InventoryItem();
            item3.ModelID = "third";
            item3.DiskInterface = "sata";
            item3.FormFactor = "2.5\"";
            item3.RotationRate = "7200rpm";
            item3.SectorSize = 520;
            UI_Inventory.Add(item3);

            InventoryItem item4 = new InventoryItem();
            item4.ModelID = "fourth";
            item4.DiskInterface = "sas";
            item4.FormFactor = "2.5\"";
            item4.RotationRate = "7200rpm";
            item4.SectorSize = 512;
            UI_Inventory.Add(item4);
        }

        public void SetUpdateMessage()
        {
            if (SaveRequired)
                UpdateMessage = "Record(s) have been updated. Click save to commit your changes.";
            else
                UpdateMessage = null;
        }
        public void SaveChanges()
        {
            UpdateMessage = null;
            SaveRequired = false;
        }
        private void SetupChangeListeners()
        {
            UI_Inventory.CollectionChanged += UI_Inventory_CollectionChanged;
            foreach (InventoryItem sp in UI_Inventory)
            {
                sp.PropertyChanged += Sp_PropertyChanged;
            }
        }

        // Just here in case it's needed later.
        private void TeardownChangeListeners()
        {
            if (UI_Inventory != null)
            {
                UI_Inventory.CollectionChanged -= UI_Inventory_CollectionChanged;
                foreach (InventoryItem sp in UI_Inventory)
                {
                    sp.PropertyChanged -= Sp_PropertyChanged;
                }
            }
        }

        private bool ChangesAreValid()
        {
            foreach (InventoryItem sp in UI_Inventory)
            {
                if (sp.ChangeType == DBChangeType.Insert || sp.ChangeType == DBChangeType.Update)
                {
                    if (String.IsNullOrEmpty(sp.ModelID))
                    {
                        DialogService.ShowError("SuperProject is missing name. Please re-enter.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetSaveRequired()
        {
            bool saveRequired = false;
            foreach (InventoryItem sp in UI_Inventory)
            {
                if (sp.ChangeType != DBChangeType.NoChange)
                {
                    saveRequired = true;
                }
            }
            SaveRequired = saveRequired;
        }

        private void Sp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveRequired = true;
            SetUpdateMessage();
        }

        private void UI_Inventory_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveRequired = true;
            SetUpdateMessage();
        }
    }
}
