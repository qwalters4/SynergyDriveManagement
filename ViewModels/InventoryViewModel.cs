using IM.Services;
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
        private List<string> possibleformfactors;
        private ObservableCollection<KVpair> activeFormFactorFilter = new ObservableCollection<KVpair>();
        private ObservableCollection<KVpair> activeBrandFilter = new ObservableCollection<KVpair>();
        private ObservableCollection<KVpair> activeConnectorFilter = new ObservableCollection<KVpair>();

        private string updateMessage = "Changes have been made, please save the results.";
        private bool saveRequired;

        public ObservableCollection<InventoryItem> UI_Inventory
        {
            get => ui_inventory;
            set
            {
                ui_inventory = value;
                OnPropertyChanged(nameof(UI_Inventory));
                SetSaveRequired();
            }
        }
        public InventoryItem SelectedItem { get => selectedItem; set => selectedItem = value; }
        public List<string> PossibleFormFactors { get => possibleformfactors; set => possibleformfactors = value; }
        public ObservableCollection<KVpair> ActiveFFFilter
        {
            get => activeFormFactorFilter;
            set
            {
                activeFormFactorFilter = value;
                OnPropertyChanged(nameof(ActiveFFFilter));
            }
        }
        public ObservableCollection<KVpair> ActiveBrandFilter
        {
            get => activeBrandFilter;
            set
            {
                activeBrandFilter = value;
                OnPropertyChanged(nameof(ActiveBrandFilter));
            }
        }
        public ObservableCollection<KVpair> ActiveConnectorFilter
        {
            get => activeConnectorFilter;
            set
            {
                activeConnectorFilter = value;
                OnPropertyChanged(nameof(ActiveConnectorFilter));
            }
        }
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

        public void Load()
        {
            model = new InventoryModel();
            model.DataService = new DataService();
            UI_Inventory = new ObservableCollection<InventoryItem>();
            PossibleFormFactors = new List<string>();
            SetupChangeListeners();
            SetSaveRequired();
            UI_Inventory = model.DataService.Query();
            ActiveBrandFilter = model.GetBrandList();
            ActiveFFFilter = model.GetFFList();
            ActiveConnectorFilter = model.GetConnectorList();
        }

        public void Refresh()
        {
            UI_Inventory = model.DataService.Query();
            ActiveBrandFilter = model.GetBrandList();
            ActiveFFFilter = model.GetFFList();
            ActiveConnectorFilter = model.GetConnectorList();
        }

        public void Filter()
        {
            List<string> ff = new List<string>();
            foreach(KVpair k in ActiveFFFilter)
            {
                if (k.test == true)
                    ff.Add(k.Key);
            }

            List<string> conn = new List<string>();
            foreach (KVpair k in ActiveConnectorFilter)
            {
                if (k.test == true)
                    conn.Add(k.Key);
            }

            List<string> brand = new List<string>();
            foreach (KVpair k in ActiveBrandFilter)
            {
                if (k.test == true)
                    brand.Add(k.Key);
            }

            UI_Inventory = model.DataService.Query(ff, conn, brand);
        }

        public void testload()
        {
            KVpair a = new KVpair("a", false);
            KVpair b = new KVpair("b", false);
            KVpair c = new KVpair("c", true);
            KVpair d = new KVpair("d", false);

            ActiveFFFilter.Add(a);
            ActiveFFFilter.Add(b);
            ActiveFFFilter.Add(c);
            ActiveFFFilter.Add(d);
            ActiveBrandFilter.Add(a);
            ActiveBrandFilter.Add(b);
            ActiveBrandFilter.Add(c);
            ActiveBrandFilter.Add(d);

            InventoryItem item1 = new InventoryItem();
            item1.ModelID = "first";
            item1.DiskInterface = "sata";
            item1.FormFactor = "2.5\"";
            item1.Capacity = 512;
            item1.ChangeType = DBChangeType.NoChange;
            UI_Inventory.Add(item1);

            InventoryItem item2 = new InventoryItem();
            item2.ModelID = "second";
            item2.DiskInterface = "sata";
            item2.FormFactor = "2.5\"";
            item2.Capacity = 512;
            item2.ChangeType = DBChangeType.NoChange;
            UI_Inventory.Add(item2);

            InventoryItem item3 = new InventoryItem();
            item3.ModelID = "third";
            item3.DiskInterface = "sata";
            item3.FormFactor = "2.5\"";
            item3.Capacity = 1000;
            item3.ChangeType = DBChangeType.NoChange;
            UI_Inventory.Add(item3);

            InventoryItem item4 = new InventoryItem();
            item4.ModelID = "fourth";
            item4.DiskInterface = "sas";
            item4.FormFactor = "2.5\"";
            item4.Capacity = 2000;
            item4.ChangeType = DBChangeType.NoChange;
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
                        DialogService.ShowError("ModelID is missing. Please re-enter.");
                        return false;
                    }
                }
            }
            return true;
        }

        public void SaveData()
        {
            if(ChangesAreValid())
            {
                List<InventoryItem> outgoing = new List<InventoryItem>();
                foreach(InventoryItem i in UI_Inventory)
                {
                    if(i.ChangeType == DBChangeType.Insert)
                        outgoing.Add(i);
                }
                model.InsertRows(outgoing);
                outgoing.Clear();
                foreach(InventoryItem i in UI_Inventory)
                {
                    if (i.ChangeType == DBChangeType.Update)
                        outgoing.Add(i);
                }
                model.UpdateRows(outgoing);
                SaveRequired = false;
            }
            else
            {
                UpdateMessage = "One or more entries is incorrect.";
            }
        }

        public void SetSaveRequired()
        {
            if (UI_Inventory == null)
                return;
            bool saveRequired = false;
            foreach (InventoryItem sp in UI_Inventory)
            {
                if (sp.ChangeType != DBChangeType.NoChange)
                {
                    saveRequired = true;
                }
            }
            SaveRequired = saveRequired;
            SetUpdateMessage();
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

        public void CloseConnection()
        {
            model.CloseConnection();
        }
    }
}
