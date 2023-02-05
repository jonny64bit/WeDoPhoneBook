import { fileURLToPath, URL } from 'node:url'
const path = require('path')

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: "wwwroot/dist",
    sourcemap: true,
    lib: {
      // Could also be a dictionary or array of multiple entry points
      entry: fileURLToPath(new URL('./src/main.ts', import.meta.url)),
      name: 'phonebook-web',
      // the proper extensions will be added
      fileName: 'phonebook-web',
    },
  },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
      '~bootstrap': path.resolve(__dirname, 'node_modules/bootstrap'),
    }
  },
  define: {
    'process.env': {}
  }
})
