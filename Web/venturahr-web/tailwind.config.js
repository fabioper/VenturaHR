module.exports = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx}",
    "./src/components/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    container: {
      center: true,
      padding: "1rem",
    },
    fontFamily: {
      body: ["Inter"],
    },
    extend: {},
  },
  plugins: [],
  corePlugins: {
    preflight: false,
    divideStyle: true,
  },
}
