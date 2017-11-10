/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 09/11/2017
 * Hora: 19:40
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PokemonScriptStudio
{
	/// <summary>
	/// Interaction logic for Configuracion.xaml
	/// </summary>
	public partial class Configuracion : UserControl
	{
		public const string PATHARCHIVOCONFIGYNOMBRE="config.txt";
		public event EventHandler VolverAlMain;
		public Configuracion()
		{
			InitializeComponent();
			Cargar();
		}
		void BtnBuscarRutaRoms_Click(object sender, RoutedEventArgs e)
		{
			txtRutaRoms.Text=BuscarDir();
			Guardar();
		}
		void BtnBuscarRutScripts_Click(object sender, RoutedEventArgs e)
		{
			txtRutaScripts.Text=BuscarDir();
			Guardar();
		}
		string BuscarDir()
		{
			System.Windows.Forms.FolderBrowserDialog fbw=new System.Windows.Forms.FolderBrowserDialog();
			string dir;
			if(fbw.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				dir=fbw.SelectedPath;
			}else dir="";
			return dir;
		}
		void Guardar()
		{
			string[] strConfig;
			if(System.IO.File.Exists(PATHARCHIVOCONFIGYNOMBRE))
					System.IO.File.Delete(PATHARCHIVOCONFIGYNOMBRE);
			
			if(!String.IsNullOrEmpty(txtRutaRoms.Text)||!String.IsNullOrEmpty(txtRutaScripts.Text)){
				strConfig=new string[2];
				strConfig[0]="Roms="+txtRutaRoms.Text;
				strConfig[1]="Scripts="+txtRutaScripts.Text;
				
				System.IO.File.AppendAllLines(PATHARCHIVOCONFIGYNOMBRE,strConfig);
			}
			if(System.IO.Directory.Exists(txtRutaRoms.Text)&&System.IO.Directory.GetFiles(txtRutaRoms.Text).Length==0&&System.IO.Directory.GetDirectories(txtRutaRoms.Text).Length==0)
				System.IO.Directory.Delete(txtRutaRoms.Text);
			if(System.IO.Directory.Exists(txtRutaScripts.Text)&&System.IO.Directory.GetFiles(txtRutaScripts.Text).Length==0&&System.IO.Directory.GetDirectories(txtRutaRoms.Text).Length==0)
				System.IO.Directory.Delete(txtRutaScripts.Text);
			
		}
		void Cargar()
		{
			string[] strConfig;
			if(System.IO.File.Exists(PATHARCHIVOCONFIGYNOMBRE))
			{
				try{
				strConfig=System.IO.File.ReadAllLines(PATHARCHIVOCONFIGYNOMBRE);
				txtRutaRoms.Text=strConfig[0].Split('=')[1];
				txtRutaScripts.Text=strConfig[1].Split('=')[1];
				}catch{
					MessageBox.Show("Hay problemas para cargar la configuración..."); 
				
				}
			}
			if(String.IsNullOrEmpty(txtRutaRoms.Text)){
				txtRutaRoms.Text=System.IO.Path.Combine(Environment.CurrentDirectory,"Roms");
				if(!System.IO.Directory.Exists(txtRutaRoms.Text))
					System.IO.Directory.CreateDirectory(txtRutaRoms.Text);
			}
			if(String.IsNullOrEmpty(txtRutaScripts.Text)){
				txtRutaScripts.Text=System.IO.Path.Combine(Environment.CurrentDirectory,"Scripts");
				if(!System.IO.Directory.Exists(txtRutaScripts.Text))
					System.IO.Directory.CreateDirectory(txtRutaScripts.Text);
			}
				
		}
		void BtnVolver_Click(object sender, RoutedEventArgs e)
		{
			this.Visibility=Visibility.Hidden;
			if(VolverAlMain!=null)
				VolverAlMain(this,new EventArgs());
		}
	}
}