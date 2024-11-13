using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace NicamicsApp
{
    public class MaskedBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null) return;

            // Remueve los espacios para mantener el texto sin formato
            var unformattedText = e.NewTextValue?.Replace(" ", "");

            // Aplica el formateo si es necesario
            if (!string.IsNullOrEmpty(unformattedText) && unformattedText.Length <= 16)
            {
                string formattedText = "";
                for (int i = 0; i < unformattedText.Length; i++)
                {
                    if (i > 0 && i % 4 == 0)
                        formattedText += " "; // Agrega un espacio cada 4 dígitos
                    formattedText += unformattedText[i];
                }

                // Evita un bucle infinito de cambio de texto
                entry.TextChanged -= OnTextChanged;
                entry.Text = formattedText;
                entry.TextChanged += OnTextChanged;
            }
        }
    }
}
