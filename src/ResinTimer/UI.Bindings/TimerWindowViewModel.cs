﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using JetBrains.Annotations;
using Livet.Messaging;
using MetroRadiance.UI;
using MetroTrilithon.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ResinTimer.Properties;

namespace ResinTimer.UI.Bindings
{
    public class TimerWindowViewModel : WindowViewModel
    {
        public const string ShowOptionWindowMessageKey = nameof(ShowOptionWindowMessageKey);
        
        public Timer Timer { get; }

        public IReactiveProperty<string> NewResin { get; }

        public IReadOnlyReactiveProperty<bool> IsOverflow { get; }

        public IReadOnlyReactiveProperty<bool> ShowInTaskbar { get; }

        public IReadOnlyReactiveProperty<bool> TopMost { get; }

        public TimerWindowViewModel()
            : this(new Timer())
        {
        }

        public TimerWindowViewModel(Timer timer)
        {
            this.Title = AssemblyInfo.Title;

            this.Timer = timer;
            this.NewResin = new ReactiveProperty<string>();
            this.IsOverflow = timer.RemainingTime
                .Select(x => x.TotalSeconds < 0)
                .ToReadOnlyReactiveProperty();

            this.IsOverflow
                .Subscribe(x => ThemeService.Current.ChangeAccent(x ? Accent.Orange : Accent.Blue));
            
            this.ShowInTaskbar = UserSettings.Default
                .ToReactivePropertyAsSynchronized(x => x.ShowInTaskbar);

            this.TopMost = UserSettings.Default
                .ToReactivePropertyAsSynchronized(x => x.TopMost);
        }

        [UsedImplicitly]
        public void Update()
        {
            if (int.TryParse(this.NewResin.Value, out var resin))
            {
                this.Timer.Reset(resin, true);
            }

            this.NewResin.Value = "";
        }

        [UsedImplicitly]
        public void Settings()
        {
            var binding = new MainWindowViewModel();
            var message = new TransitionMessage(binding, ShowOptionWindowMessageKey);
            this.Messenger.Raise(message);
        }
    }
}
