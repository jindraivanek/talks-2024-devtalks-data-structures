import type { NavOperations, ShortcutOptions } from '@slidev/types'
import { defineShortcutsSetup } from '@slidev/types'

export default defineShortcutsSetup((nav: NavOperations, base: ShortcutOptions[]) => {
  return [
    ...base, // keep the existing shortcuts
    {
      key: 'down',
      fn: () => nav.next(),
      autoRepeat: true,
    },
    {
      key: 'up',
      fn: () => nav.prev(),
      autoRepeat: true,
    },
  ]
})