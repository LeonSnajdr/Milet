/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}", "./node_modules/flowbite/**/*.js"],
  theme: {
    screens: {
      tablet: "960px",
      desktop: "1248px"
    },
    colors: {
      base: "#000",
      white: "#fff",
      primary: "#8447FF",
      secondary: "#999A9E",
      accent: "#1C1C1E",
      red: "#FD3D30"
    },
    extend: {}
  },
  plugins: [],
  darkMode: "class"
};
