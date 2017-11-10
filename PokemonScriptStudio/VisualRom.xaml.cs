/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 10/11/2017
 * Hora: 11:14
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
using PokemonGBAFrameWork;
namespace PokemonScriptStudio
{
	/// <summary>
	/// Interaction logic for VisualRom.xaml
	/// </summary>
	public partial class VisualRom : UserControl
	{
		string pathRom;
		EdicionPokemon edicion;
		RomData romData;
		public event EventHandler Seleccionado;
		public event EventHandler DesSeleccionado;
		public VisualRom(string pathRom)
		{
			InitializeComponent();
			this.pathRom = pathRom;
			EstaSeleccionado=false;
			CargarInterficie();
		}

		public RomGba Rom {
			get {
				return RomData.Rom;
			}
			set {
				pathRom = value.FullPath;
				edicion = null;
				romData = null;
				CargarInterficie();
			}
		}

		public EdicionPokemon Edicion {
			get {
				return RomData.Edicion;
			}
		}

		public RomData RomData {
			get {
				if (romData == null)
					romData = new RomData(pathRom);
				return romData;
			}
		}
		public bool EstaSeleccionado {
			get{ return Background.Equals(Brushes.AliceBlue); }
			set {
				if (value)
					Background = Brushes.AliceBlue;
				else
					Background = Brushes.Transparent;
			
			}
		}
		void CargarInterficie()
		{
			txtNombreRom.Text = Rom.Nombre;
			switch (RomData.Edicion.AbreviacionRom) {
				case AbreviacionCanon.AXV:
					break;
				case AbreviacionCanon.AXP:
					break;
				case AbreviacionCanon.BPE:
					break;
				case AbreviacionCanon.BPR:
					break;
				case AbreviacionCanon.BPG:
					break;
			}
			RomData.UnLoad();
		}
		void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			EstaSeleccionado = !EstaSeleccionado;
			if (EstaSeleccionado) {
				if (Seleccionado != null)
					Seleccionado(this, new EventArgs());
			} else {
			
				if (DesSeleccionado != null)
					DesSeleccionado(this, new EventArgs());
			}
		}
	}
}