
// This file has been generated by the GUI designer. Do not modify.
namespace Pinta
{
	public partial class ResizeCanvasDialog
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox1;
		private global::Gtk.RadioButton percentageRadio;
		private global::Gtk.SpinButton percentageSpinner;
		private global::Gtk.Label label1;
		private global::Gtk.HBox hbox2;
		private global::Gtk.RadioButton absoluteRadio;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label2;
		private global::Gtk.SpinButton widthSpinner;
		private global::Gtk.Label label4;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Label label3;
		private global::Gtk.SpinButton heightSpinner;
		private global::Gtk.Label label5;
		private global::Gtk.CheckButton aspectCheckbox;
		private global::Gtk.HBox hbox5;
		private global::Gtk.Alignment alignment1;
		private global::Gtk.Table table1;
		private global::Gtk.Button CenterButton;
		private global::Gtk.Button EButton;
		private global::Gtk.Button NButton;
		private global::Gtk.Button NEButton;
		private global::Gtk.Button NWButton;
		private global::Gtk.Button SButton;
		private global::Gtk.Button SEButton;
		private global::Gtk.Button SWButton;
		private global::Gtk.Button WButton;
		private global::Gtk.Alignment alignment2;
		private global::Gtk.Label label6;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Pinta.ResizeCanvasDialog
			this.Name = "Pinta.ResizeCanvasDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Resize Canvas");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			this.DefaultWidth = 300;
			this.DefaultHeight = 200;
			// Internal child Pinta.ResizeCanvasDialog.VBox
			global::Gtk.Box w1 = this.ContentArea;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			this.vbox2.BorderWidth = ((uint)(12));
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.percentageRadio = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("By percentage:"));
			this.percentageRadio.CanFocus = true;
			this.percentageRadio.Name = "percentageRadio";
			this.percentageRadio.DrawIndicator = true;
			this.percentageRadio.UseUnderline = true;
//			this.percentageRadio.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.hbox1.Add (this.percentageRadio);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.percentageRadio]));
			w2.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.percentageSpinner = new global::Gtk.SpinButton (1, 1000, 1);
			this.percentageSpinner.CanFocus = true;
			this.percentageSpinner.Name = "percentageSpinner";
			this.percentageSpinner.Adjustment.PageIncrement = 10;
			this.percentageSpinner.ClimbRate = 1;
			this.percentageSpinner.Numeric = true;
			this.percentageSpinner.Value = 100;
			this.hbox1.Add (this.percentageSpinner);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.percentageSpinner]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = "%";
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.absoluteRadio = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("By absolute size:"));
			this.absoluteRadio.CanFocus = true;
			this.absoluteRadio.Name = "absoluteRadio";
			this.absoluteRadio.DrawIndicator = true;
			this.absoluteRadio.UseUnderline = true;
			this.absoluteRadio.Group = this.percentageRadio.Group;
			this.hbox2.Add (this.absoluteRadio);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.absoluteRadio]));
			w6.Position = 0;
			this.vbox2.Add (this.hbox2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Width:");
			this.hbox3.Add (this.label2);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label2]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.widthSpinner = new global::Gtk.SpinButton (1, 10000, 1);
			this.widthSpinner.Sensitive = false;
			this.widthSpinner.CanFocus = true;
			this.widthSpinner.Name = "widthSpinner";
			this.widthSpinner.Adjustment.PageIncrement = 10;
			this.widthSpinner.ClimbRate = 1;
			this.widthSpinner.Numeric = true;
			this.hbox3.Add (this.widthSpinner);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.widthSpinner]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("pixels");
			this.hbox3.Add (this.label4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label4]));
			w10.Position = 2;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox3]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Height:");
			this.hbox4.Add (this.label3);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label3]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.heightSpinner = new global::Gtk.SpinButton (1, 10000, 1);
			this.heightSpinner.Sensitive = false;
			this.heightSpinner.CanFocus = true;
			this.heightSpinner.Name = "heightSpinner";
			this.heightSpinner.Adjustment.PageIncrement = 10;
			this.heightSpinner.ClimbRate = 1;
			this.heightSpinner.Numeric = true;
			this.hbox4.Add (this.heightSpinner);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.heightSpinner]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("pixels");
			this.hbox4.Add (this.label5);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label5]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			this.vbox2.Add (this.hbox4);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox4]));
			w15.Position = 3;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.aspectCheckbox = new global::Gtk.CheckButton ();
			this.aspectCheckbox.Sensitive = false;
			this.aspectCheckbox.CanFocus = true;
			this.aspectCheckbox.Name = "aspectCheckbox";
			this.aspectCheckbox.Label = global::Mono.Unix.Catalog.GetString ("Maintain aspect ratio");
			this.aspectCheckbox.Active = true;
			this.aspectCheckbox.DrawIndicator = true;
			this.aspectCheckbox.UseUnderline = true;
			this.vbox2.Add (this.aspectCheckbox);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.aspectCheckbox]));
			w16.Position = 4;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.hbox5.Add (this.alignment1);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.alignment1]));
			w17.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.CenterButton = new global::Gtk.Button ();
			this.CenterButton.WidthRequest = 30;
			this.CenterButton.HeightRequest = 30;
			this.CenterButton.CanFocus = true;
			this.CenterButton.Name = "CenterButton";
			this.CenterButton.UseUnderline = true;
			// Container child CenterButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w18 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w19 = new global::Gtk.HBox ();
			w19.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w20 = new global::Gtk.Image ();
			w19.Add (w20);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w22 = new global::Gtk.Label ();
			w19.Add (w22);
			w18.Add (w19);
			this.CenterButton.Add (w18);
			this.table1.Add (this.CenterButton);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1 [this.CenterButton]));
			w26.TopAttach = ((uint)(1));
			w26.BottomAttach = ((uint)(2));
			w26.LeftAttach = ((uint)(1));
			w26.RightAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.EButton = new global::Gtk.Button ();
			this.EButton.WidthRequest = 30;
			this.EButton.HeightRequest = 30;
			this.EButton.CanFocus = true;
			this.EButton.Name = "EButton";
			this.EButton.UseUnderline = true;
			// Container child EButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w27 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w28 = new global::Gtk.HBox ();
			w28.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w29 = new global::Gtk.Image ();
			w28.Add (w29);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w31 = new global::Gtk.Label ();
			w28.Add (w31);
			w27.Add (w28);
			this.EButton.Add (w27);
			this.table1.Add (this.EButton);
			global::Gtk.Table.TableChild w35 = ((global::Gtk.Table.TableChild)(this.table1 [this.EButton]));
			w35.TopAttach = ((uint)(1));
			w35.BottomAttach = ((uint)(2));
			w35.LeftAttach = ((uint)(2));
			w35.RightAttach = ((uint)(3));
			w35.XOptions = ((global::Gtk.AttachOptions)(4));
			w35.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.NButton = new global::Gtk.Button ();
			this.NButton.WidthRequest = 30;
			this.NButton.HeightRequest = 30;
			this.NButton.CanFocus = true;
			this.NButton.Name = "NButton";
			this.NButton.UseUnderline = true;
			// Container child NButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w36 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w37 = new global::Gtk.HBox ();
			w37.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w38 = new global::Gtk.Image ();
			w37.Add (w38);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w40 = new global::Gtk.Label ();
			w37.Add (w40);
			w36.Add (w37);
			this.NButton.Add (w36);
			this.table1.Add (this.NButton);
			global::Gtk.Table.TableChild w44 = ((global::Gtk.Table.TableChild)(this.table1 [this.NButton]));
			w44.LeftAttach = ((uint)(1));
			w44.RightAttach = ((uint)(2));
			w44.XOptions = ((global::Gtk.AttachOptions)(4));
			w44.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.NEButton = new global::Gtk.Button ();
			this.NEButton.WidthRequest = 30;
			this.NEButton.HeightRequest = 30;
			this.NEButton.CanFocus = true;
			this.NEButton.Name = "NEButton";
			this.NEButton.UseUnderline = true;
			// Container child NEButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w45 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w46 = new global::Gtk.HBox ();
			w46.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w47 = new global::Gtk.Image ();
			w46.Add (w47);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w49 = new global::Gtk.Label ();
			w46.Add (w49);
			w45.Add (w46);
			this.NEButton.Add (w45);
			this.table1.Add (this.NEButton);
			global::Gtk.Table.TableChild w53 = ((global::Gtk.Table.TableChild)(this.table1 [this.NEButton]));
			w53.LeftAttach = ((uint)(2));
			w53.RightAttach = ((uint)(3));
			w53.XOptions = ((global::Gtk.AttachOptions)(4));
			w53.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.NWButton = new global::Gtk.Button ();
			this.NWButton.WidthRequest = 30;
			this.NWButton.HeightRequest = 30;
			this.NWButton.CanFocus = true;
			this.NWButton.Name = "NWButton";
			this.NWButton.UseUnderline = true;
			// Container child NWButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w54 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w55 = new global::Gtk.HBox ();
			w55.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w56 = new global::Gtk.Image ();
			w55.Add (w56);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w58 = new global::Gtk.Label ();
			w55.Add (w58);
			w54.Add (w55);
			this.NWButton.Add (w54);
			this.table1.Add (this.NWButton);
			global::Gtk.Table.TableChild w62 = ((global::Gtk.Table.TableChild)(this.table1 [this.NWButton]));
			w62.XOptions = ((global::Gtk.AttachOptions)(4));
			w62.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.SButton = new global::Gtk.Button ();
			this.SButton.WidthRequest = 30;
			this.SButton.HeightRequest = 30;
			this.SButton.CanFocus = true;
			this.SButton.Name = "SButton";
			this.SButton.UseUnderline = true;
			// Container child SButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w63 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w64 = new global::Gtk.HBox ();
			w64.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w65 = new global::Gtk.Image ();
			w64.Add (w65);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w67 = new global::Gtk.Label ();
			w64.Add (w67);
			w63.Add (w64);
			this.SButton.Add (w63);
			this.table1.Add (this.SButton);
			global::Gtk.Table.TableChild w71 = ((global::Gtk.Table.TableChild)(this.table1 [this.SButton]));
			w71.TopAttach = ((uint)(2));
			w71.BottomAttach = ((uint)(3));
			w71.LeftAttach = ((uint)(1));
			w71.RightAttach = ((uint)(2));
			w71.XOptions = ((global::Gtk.AttachOptions)(4));
			w71.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.SEButton = new global::Gtk.Button ();
			this.SEButton.WidthRequest = 30;
			this.SEButton.HeightRequest = 30;
			this.SEButton.CanFocus = true;
			this.SEButton.Name = "SEButton";
			this.SEButton.UseUnderline = true;
			// Container child SEButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w72 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w73 = new global::Gtk.HBox ();
			w73.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w74 = new global::Gtk.Image ();
			w73.Add (w74);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w76 = new global::Gtk.Label ();
			w73.Add (w76);
			w72.Add (w73);
			this.SEButton.Add (w72);
			this.table1.Add (this.SEButton);
			global::Gtk.Table.TableChild w80 = ((global::Gtk.Table.TableChild)(this.table1 [this.SEButton]));
			w80.TopAttach = ((uint)(2));
			w80.BottomAttach = ((uint)(3));
			w80.LeftAttach = ((uint)(2));
			w80.RightAttach = ((uint)(3));
			w80.XOptions = ((global::Gtk.AttachOptions)(4));
			w80.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.SWButton = new global::Gtk.Button ();
			this.SWButton.WidthRequest = 30;
			this.SWButton.HeightRequest = 30;
			this.SWButton.CanFocus = true;
			this.SWButton.Name = "SWButton";
			this.SWButton.UseUnderline = true;
			// Container child SWButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w81 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w82 = new global::Gtk.HBox ();
			w82.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w83 = new global::Gtk.Image ();
			w82.Add (w83);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w85 = new global::Gtk.Label ();
			w82.Add (w85);
			w81.Add (w82);
			this.SWButton.Add (w81);
			this.table1.Add (this.SWButton);
			global::Gtk.Table.TableChild w89 = ((global::Gtk.Table.TableChild)(this.table1 [this.SWButton]));
			w89.TopAttach = ((uint)(2));
			w89.BottomAttach = ((uint)(3));
			w89.XOptions = ((global::Gtk.AttachOptions)(4));
			w89.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.WButton = new global::Gtk.Button ();
			this.WButton.WidthRequest = 30;
			this.WButton.HeightRequest = 30;
			this.WButton.CanFocus = true;
			this.WButton.Name = "WButton";
			this.WButton.UseUnderline = true;
			// Container child WButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w90 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w91 = new global::Gtk.HBox ();
			w91.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w92 = new global::Gtk.Image ();
			w91.Add (w92);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w94 = new global::Gtk.Label ();
			w91.Add (w94);
			w90.Add (w91);
			this.WButton.Add (w90);
			this.table1.Add (this.WButton);
			global::Gtk.Table.TableChild w98 = ((global::Gtk.Table.TableChild)(this.table1 [this.WButton]));
			w98.TopAttach = ((uint)(1));
			w98.BottomAttach = ((uint)(2));
			w98.XOptions = ((global::Gtk.AttachOptions)(4));
			w98.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox5.Add (this.table1);
			global::Gtk.Box.BoxChild w99 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.table1]));
			w99.Position = 1;
			w99.Expand = false;
			w99.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.hbox5.Add (this.alignment2);
			global::Gtk.Box.BoxChild w100 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.alignment2]));
			w100.Position = 2;
			this.vbox2.Add (this.hbox5);
			global::Gtk.Box.BoxChild w101 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox5]));
			w101.PackType = ((global::Gtk.PackType)(1));
			w101.Position = 5;
			w101.Expand = false;
			w101.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.Xalign = 0F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Anchor:");
			this.vbox2.Add (this.label6);
			global::Gtk.Box.BoxChild w102 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label6]));
			w102.PackType = ((global::Gtk.PackType)(1));
			w102.Position = 6;
			w102.Expand = false;
			w102.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox2.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w103 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator1]));
			w103.PackType = ((global::Gtk.PackType)(1));
			w103.Position = 7;
			w103.Expand = false;
			w103.Fill = false;
			w1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w104 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox2]));
			w104.Position = 0;
			w104.Expand = false;
			w104.Fill = false;
			// Internal child Pinta.ResizeCanvasDialog.ActionArea
			global::Gtk.ButtonBox w105 = this.ActionArea;
			w105.Name = "dialog1_ActionArea";
			w105.Spacing = 10;
			w105.BorderWidth = ((uint)(5));
			w105.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w106 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w105 [this.buttonCancel]));
			w106.Expand = false;
			w106.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w107 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w105 [this.buttonOk]));
			w107.Position = 1;
			w107.Expand = false;
			w107.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
		}
	}
}
