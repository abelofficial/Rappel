import { Grid, Input, Text, Textarea } from "@nextui-org/react";
import { useField } from "formik";
import React from "react";
import { v4 as uuid } from "uuid";
export interface TextFieldProps {
  type: string;
  name: string;
  label: string;
  fullWidth: boolean;
  placeholder?: string;
}

const Index = ({ label, ...restProps }: TextFieldProps) => {
  const [field, meta, helpers] = useField(restProps);

  return (
    <Grid
      xs={12}
      css={{
        display: "flex",
        flexDirection: "column",
      }}
    >
      <label title={label}>
        {label}
        <Input
          {...field}
          {...restProps}
          id={uuid()}
          aria-label={`${restProps.name} input field`}
          status={meta.touched && meta.error ? "error" : "default"}
        />
      </label>
      {meta.touched && meta.error ? (
        <Text color='error'>{meta.error}</Text>
      ) : null}
    </Grid>
  );
};

export const TextAreaField = ({ label, ...restProps }: TextFieldProps) => {
  const [field, meta, helpers] = useField(restProps);

  return (
    <Grid
      xs={12}
      css={{
        display: "flex",
        flexDirection: "column",
      }}
    >
      <label title={label}>
        {label}
        <Textarea
          {...field}
          {...restProps}
          id={uuid()}
          aria-label={`${restProps.name} input field`}
          status={meta.touched && meta.error ? "error" : "default"}
        />
      </label>
      {meta.touched && meta.error ? (
        <Text color='error'>{meta.error}</Text>
      ) : null}
    </Grid>
  );
};

export default Index;
