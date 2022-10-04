module.exports = {
  root: true,
  env: {
    node: true,
    "vue/setup-compiler-macros": true
  },
  extends: [
    "plugin:vue/vue3-strongly-recommended",
    "eslint:recommended",
    "@vue/typescript/recommended",
    "./.eslintsag",
    "plugin:prettier/recommended"
  ],
  parserOptions: {
    ecmaVersion: 2020
  },
  rules: {
    "no-console": "off",
    "no-debugger": "off"
  },
  overrides: [
    {
      files: [
        "**/__tests__/*.{j,t}s?(x)",
        "**/tests/unit/**/*.spec.{j,t}s?(x)"
      ],
      env: {
        jest: true
      }
    }
  ]
};
